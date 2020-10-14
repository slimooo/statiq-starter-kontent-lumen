//using Kentico.Kontent.Delivery.Abstractions;
//using Kentico.Kontent.Statiq.Lumen.Models;
//using Kontent.Statiq;
//using Statiq.Common;
//using Statiq.Core;

//namespace Kentico.Kontent.Statiq.Lumen.Pipelines
//{
//    public abstract class GenericPipeline<TModel> : Pipeline where TModel : class
//    {
//        public GenericPipeline(IDeliveryClient deliveryClient)
//        {
//            InputModules = new ModuleList{
//                new Kontent<TModel>(deliveryClient),
//                new SetDestination(Config.FromDocument((doc, ctx)  => new NormalizedPath($"{doc.AsKontent<TModel>()}.html" ))),
//            };

//            ProcessModules = new ModuleList
//            {
//            };

//            OutputModules = new ModuleList {
//                new WriteFiles(),
//            };
//        }
//    }
//}