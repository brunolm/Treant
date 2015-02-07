namespace Treant.Core
{
    using System;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;

    public static class MefBootstrap
    {
        private static CompositionContainer container;

        public static CompositionContainer Container
        {
            get
            {
                if (container == null)
                {
                    var catalog = new DirectoryCatalog(".", "Treant.*");

                    container = new CompositionContainer(catalog);
                }

                return container;
            }
        }

        public static object Create(Type type)
        {
            var getExportedValue = Container.GetType().GetMethod("GetExportedValueOrDefault", new Type[] { });
            var exportedValue = getExportedValue.MakeGenericMethod(new Type[] { type }).Invoke(Container, null);

            if (exportedValue == null)
            {
                exportedValue = Activator.CreateInstance(type);
            }

            Container.ComposeParts(exportedValue);
            Container.SatisfyImportsOnce(exportedValue);

            return exportedValue;
        }

        public static T Resolve<T>()
        {
            return Container.GetExportedValueOrDefault<T>();
        }
    }
}
