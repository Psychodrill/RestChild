﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestChild.Booking.Logic.WcfSearchService {


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WcfSearchService.ITourSearchService")]
    public interface ITourSearchService {

        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITourSearchService/AddOrUpdateTours", ReplyAction="http://tempuri.org/ITourSearchService/AddOrUpdateToursResponse")]
        void AddOrUpdateTours(long[] tourIds);

        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITourSearchService/AddOrUpdateTours", ReplyAction="http://tempuri.org/ITourSearchService/AddOrUpdateToursResponse")]
        System.Threading.Tasks.Task AddOrUpdateToursAsync(long[] tourIds);

        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITourSearchService/AddOrUpdateTour", ReplyAction="http://tempuri.org/ITourSearchService/AddOrUpdateTourResponse")]
        void AddOrUpdateTour(long tourId);

        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITourSearchService/AddOrUpdateTour", ReplyAction="http://tempuri.org/ITourSearchService/AddOrUpdateTourResponse")]
        System.Threading.Tasks.Task AddOrUpdateTourAsync(long tourId);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITourSearchServiceChannel : RestChild.Booking.Logic.WcfSearchService.ITourSearchService, System.ServiceModel.IClientChannel {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TourSearchServiceClient : System.ServiceModel.ClientBase<RestChild.Booking.Logic.WcfSearchService.ITourSearchService>, RestChild.Booking.Logic.WcfSearchService.ITourSearchService {

        public TourSearchServiceClient() {
        }

        public TourSearchServiceClient(string endpointConfigurationName) :
                base(endpointConfigurationName) {
        }

        public TourSearchServiceClient(string endpointConfigurationName, string remoteAddress) :
                base(endpointConfigurationName, remoteAddress) {
        }

        public TourSearchServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
                base(endpointConfigurationName, remoteAddress) {
        }

        public TourSearchServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress) {
        }

        public void AddOrUpdateTours(long[] tourIds) {
            base.Channel.AddOrUpdateTours(tourIds);
        }

        public System.Threading.Tasks.Task AddOrUpdateToursAsync(long[] tourIds) {
            return base.Channel.AddOrUpdateToursAsync(tourIds);
        }

        public void AddOrUpdateTour(long tourId) {
            base.Channel.AddOrUpdateTour(tourId);
        }

        public System.Threading.Tasks.Task AddOrUpdateTourAsync(long tourId) {
            return base.Channel.AddOrUpdateTourAsync(tourId);
        }
    }
}
