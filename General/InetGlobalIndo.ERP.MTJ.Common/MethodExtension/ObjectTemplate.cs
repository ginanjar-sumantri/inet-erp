namespace InetGlobalIndo.ERP.MTJ.Common.MethodExtension
{
    public static class ObjectTemplate
    {
        public static T Template<T>(this object _prmObject, T example)
        {
            return (T)_prmObject;
        }
    }
}