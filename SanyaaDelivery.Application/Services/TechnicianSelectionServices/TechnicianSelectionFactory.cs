using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Application.Services.TechnicianSelectionServices
{
    public class TechnicianSelectionFactory
    {
        static TechnicianSelectionType selectionTypeSetting;
        public static TechnicianSelectionType TechnicianSelectionType => selectionTypeSetting;
        static TechnicianSelectionFactory()
        {
            var selectionSetting = GeneralSetting.GetSettingValue("TechnicianSelectionType");
            if (string.IsNullOrEmpty(selectionSetting))
            {
                selectionTypeSetting = TechnicianSelectionType.BroadcastAll;
            }
            selectionTypeSetting = (TechnicianSelectionType)(Convert.ToInt32(selectionSetting));
        }

        public static ITechnicianSelection GetSelectionService(TechnicianSelectionType type = TechnicianSelectionType.None)
        {
            if(type == TechnicianSelectionType.None)
            {
                type = selectionTypeSetting;
            }
            switch (type)
            {
                case TechnicianSelectionType.App:
                    return new AppTechnicianSelectionService();
                case TechnicianSelectionType.BroadcastAll:
                    return new BroadcastAllTechnicianSelectionService();
                case TechnicianSelectionType.BroadcastFavourite:
                    return new AppTechnicianSelectionService();
                case TechnicianSelectionType.SentToApprove:
                    return new AskApproveTechnicianSelectionService();
                default:
                    return new BroadcastAllTechnicianSelectionService();
            }
        }

    }
}
