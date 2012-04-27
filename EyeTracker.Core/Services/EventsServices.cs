using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common;
using EyeTracker.Domain.Model.Events;
using EyeTracker.Domain.Repositories;
using EyeTracker.Common.Logger;
using System.Reflection;

namespace EyeTracker.Core.Services
{

    /// <summary>
    /// expose event services 
    /// 4. HandlePackageEvent
    /// </summary>
    public interface IEventsServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewPartEvent"></param>
        /// <returns></returns>
        OperationResult AddViewPartEvents(IEnumerable<ViewPartEvent> viewPartEvent);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clickEvents"></param>
        /// <returns></returns>
        OperationResult AddClickEvents(IEnumerable<ClickEvent> clickEvents);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitEvents"></param>
        /// <returns></returns>
        OperationResult<long> AddVisitEvent(VisitEvent visitEvents);
    
        /// <summary>
        /// Handle package event will procced operations with PackageEvent
        /// </summary>
        /// <param name="packageEvent"></param>
        /// <returns>OK - if succeeded</returns>
        OperationResult HandlePackageEvent(PackageEvent packageEvent);
    }

    public class EventsServices : IEventsServices
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        private IEventsRepository eventRepository = null;
        private IDateRepository dataRepository = null;

        public EventsServices()
            : this(new EventsRepository(), new DataRepository())
        {
        }

        public EventsServices(IEventsRepository eventRepository, IDateRepository dataRepository)
        {
            this.eventRepository = eventRepository;
            this.dataRepository = dataRepository;
        }

        #region Old Code 
        public OperationResult<long> AddVisitEvent(VisitEvent visitEvent)
        {
            try
            {
                //TODO: Add events writing by thread and queue
                long eventId = eventRepository.AddVisitEvent(visitEvent);
                //TODO: get country and city by ip
                string country = string.Empty;
                string city = string.Empty;
                visitEvent.Id = eventId;
                dataRepository.ParseVisitEvent(visitEvent, country, city);
                return new OperationResult<long>(eventId);
            }
            catch (Exception exp)
            {
                return new OperationResult<long>(exp);
            }
        }

        public OperationResult AddViewPartEvents(IEnumerable<ViewPartEvent> viewPartEvents)
        {
            try
            {
                eventRepository.AddViewPartEvents(viewPartEvents);
                dataRepository.ParseViewPartEvents(viewPartEvents);
                return new OperationResult();
            }
            catch (Exception exp)
            {
                return new OperationResult(exp);
            }
        }

        public OperationResult AddClickEvents(IEnumerable<ClickEvent> clickEvents)
        {
            try
            {
                eventRepository.AddClickEvents(clickEvents);
                dataRepository.ParseClickEvents(clickEvents);
                return new OperationResult();
            }
            catch (Exception exp)
            {
                return new OperationResult(exp);
            }
        }
        #endregion Old Code

        public OperationResult HandlePackageEvent(PackageEvent packageEvent)
        {
            OperationResult opResult = null;
            try
            {
                eventRepository.AddPackageEvent(packageEvent);
                opResult = new OperationResult();
            }
            catch (Exception ex)
            {
                log.WriteError(ex, "Error saving package event");
                opResult = new OperationResult(ex);
            }
            return opResult;
        }

        #region IEventsServices Members

       
        #endregion
    }

    #region OldCode
    /*
     
    public interface IEventsServices
    {

        OperationResult AddViewPartEvents(IEnumerable<ViewPartEvent> viewPartEvent);

        OperationResult AddClickEvents(IEnumerable<ClickEvent> clickEvents);

        OperationResult<long> AddVisitEvent(VisitEvent visitEvents);
    
        OperationResult HandlePackageEvent(PackageEvent packageEvent);
    
    }

    public class EventsServices : IEventsServices
    {
        private IEventsRepository eventRepository = null;
        private IDateRepository dataRepository = null;

        public EventsServices()
            : this(new EventsRepository(), new DataRepository())
        {
        }

        public EventsServices(IEventsRepository eventRepository, IDateRepository dataRepository)
        {
            this.eventRepository = eventRepository;
            this.dataRepository = dataRepository;
        }

        public OperationResult<long> AddVisitEvent(VisitEvent visitEvent)
        {
            try
            {
                //TODO: Add events writing by thread and queue
                long eventId = eventRepository.AddVisitEvent(visitEvent);
                //TODO: get country and city by ip
                string country = string.Empty;
                string city = string.Empty;
                visitEvent.Id = eventId;
                dataRepository.ParseVisitEvent(visitEvent, country, city);
                return new OperationResult<long>(eventId);
            }
            catch (Exception exp)
            {
                return new OperationResult<long>(exp);
            }
        }

        public OperationResult AddViewPartEvents(IEnumerable<ViewPartEvent> viewPartEvents)
        {
            try
            {
                eventRepository.AddViewPartEvents(viewPartEvents);
                dataRepository.ParseViewPartEvents(viewPartEvents);
                return new OperationResult();
            }
            catch (Exception exp)
            {
                return new OperationResult(exp);
            }
        }

        public OperationResult AddClickEvents(IEnumerable<ClickEvent> clickEvents)
        {
            try
            {
                eventRepository.AddClickEvents(clickEvents);
                dataRepository.ParseClickEvents(clickEvents);
                return new OperationResult();
            }
            catch (Exception exp)
            {
                return new OperationResult(exp);
            }
        }
    }
     
     */
    #endregion
}
