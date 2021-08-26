using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace RestChild.Comon.Services
{
    public class NullableWebHttpBehavior : WebHttpBehavior
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.ServiceModel.Description.WebHttpBehavior" /> class.
        /// </summary>
        public NullableWebHttpBehavior()
        {
            HelpEnabled = true;
        }

        protected override QueryStringConverter GetQueryStringConverter(OperationDescription operationDescription)
        {
            return new NullableQueryStringConverter();
        }
    }
}
