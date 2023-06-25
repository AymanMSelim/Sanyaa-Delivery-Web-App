using SanyaaDelivery.Domain.LightModels;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.OtherModels
{
    public class RuntimeData
    {
        public  List<EmployeeLight> EmployeeLightList { get; set; }
        public  List<ClientLight> ClientLightList { get; set; }
        public  List<DepartmentLight> DepartmentLightList { get; set; }
        public  List<DepartmentSub0Light> DepartmentSub0LightList { get; set; }
        public  List<DepartmentSub1Light> DepartmentSub1LightList { get; set; }
        public  List<BranchLight> BranchLightList { get; set; }
        public  List<Domain.Models.ServiceT> ServiceList { get; set; }
        public  List<Domain.Models.EmployeeSubscriptionT> EmployeeSubscriptionList { get; set; }
        public  List<CountryLight> CountryLightList { get; set; }
        public  List<GovernorateLight> GovernorateLightList { get; set; }
        public  List<CityLight> CityLightList { get; set; }
        public  List<RegionLight> RegionLightList { get; set; }
        public  List<PhoneLight> PhoneLightList { get; set; }
        public  List<TranslatorT> TranslatorList { get; set; }
        public  List<EmployeeTypeT> EmployeeTypeList { get; set; }
        public  List<ServiceRatioT> ServiceRatioList { get; set; }
        public  List<SystemUserLight> SystemUserLightList { get; set; }
        public  List<PromocodeLight> PromocodeLightList { get; set; }
        public  List<SubscriptionLight> SubsctiptionLighttList { get; set; }
        public  List<SiteLight> SiteLighttList { get; set; }
        public  List<EmployeeDepartmentLight> EmployeeDepartmentLightList { get; set; }
        public  List<EmployeeWorkplaceLight> EmployeeWorkplaceLightList { get; set; }
        public  List<RequestStatusT> RequestStatusList { get; set; }
        public  List<string> FiredEmployeeIdList { get; set; }
    }
}
