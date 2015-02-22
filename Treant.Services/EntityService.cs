namespace Treant.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using Treant.Core;
    using Treant.DataProvider;
    using Treant.Domain;

    public abstract class EntityService
    {
        public virtual OperationResult Save<T>(T obj) where T : Entity
        {
            var result = new OperationResult(Validate(obj));

            using (var db = MefBootstrap.Resolve<ApplicationDbContext>())
            {
                db.Configuration.ValidateOnSaveEnabled = false;

                var dbEntity = db.Set<T>().Find(obj.ID);

                if (dbEntity == null)
                {
                    db.Set<T>().Add(obj);
                }
                else
                {
                    db.Entry(dbEntity).CurrentValues.SetValues(obj);
                    dbEntity.UpdatedDate = DateTime.UtcNow;
                }

                result.AffectedResults = db.SaveChanges();
            }

            return result;
        }

        public virtual bool Remove<T>(T obj) where T : Entity
        {
            using (var db = MefBootstrap.Resolve<ApplicationDbContext>())
            {
                db.Set<T>().Attach(obj);
                obj.Deleted = true;

                try
                {
                    return db.SaveChanges() > 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        public virtual ICollection<ValidationResult> Validate<T>(T obj) where T : Entity
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);

            return results;
        }

        public static T Clone<T>(T source) where T : Entity
        {
            if (source == null)
                return null;

            if (source == default(T))
                return default(T);

            using (var db = MefBootstrap.Resolve<ApplicationDbContext>())
            {
                db.Set<T>().Attach(source);
                return db.Entry(source).OriginalValues.ToObject() as T;
            }
        }
    }
}
