namespace AspireDemo.ApiService.Client.Models
{
    public enum VPMEventGroup
    {
        Undefined,
        Weld,
        Valve,
        MetalLoss,
        Geometry,
        General,
    }

    public static class VPMEventGroupExtensions
    {
        public static VPMEventGroup ToGroup(this VPMEvent eventItem) => (VPMEventGroup)eventItem.VPMEventTypeId;
        public static VPMEventGroup ToGroup(this VPMEventType eventType) => (VPMEventGroup)eventType.Id;
        public static string ToGroupName(this VPMEvent eventItem) => ((VPMEventGroup)eventItem.VPMEventTypeId).ToString();
        public static string ToGroupName(this VPMEventType eventType) => ((VPMEventGroup)eventType.Id).ToString();

        public static bool IsFullPipe(this VPMEventGroup group)
        {
            return group switch
            {
                VPMEventGroup.Weld or VPMEventGroup.Valve => true,
                _ => false,
            };
        }

        public static int ToZIndex(this VPMEventGroup group)
        {
            return group switch
            {
                VPMEventGroup.Undefined => 2,
                VPMEventGroup.Weld => 0,
                VPMEventGroup.Valve => 1,
                VPMEventGroup.MetalLoss => 2,
                VPMEventGroup.Geometry => 2,
                VPMEventGroup.General => 2,
                _ => 2,
            };
        }
    }

}
