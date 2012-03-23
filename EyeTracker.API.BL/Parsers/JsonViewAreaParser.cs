using EyeTracker.API.BL.Contract;
using EyeTracker.Domain.Model.Events;
namespace EyeTracker.API.BL.Parsers
{

    /// <summary>
    /// MEdiator design pattern 
    /// Concrete Collegue
    /// </summary>
    public class JsonViewAreaParser : IParser
    {
        #region IParser Members

        public object ParseToEvent(IPackage package, IEvent parentEvent)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}