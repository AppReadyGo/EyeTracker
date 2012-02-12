using EyeTracker.API.BL.Contract;
namespace EyeTracker.API.BL.Parsers
{

    /// <summary>
    /// MEdiator design pattern 
    /// Concrete Collegue
    /// </summary>
    public class JsonScrollParser : IParser
    {
        #region IParser Members

        public object ParseToEvent(IPackage package)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}