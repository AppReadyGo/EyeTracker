using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common;
using EyeTracker.Domain.Model.Events;
using EyeTracker.Domain.Repository;

namespace EyeTracker.Core.Services
{
    public interface IEventsServices
    {
        OperationResult AddViewPartEvents(IEnumerable<ViewPartEvent> viewPartEvent);

        OperationResult AddClickEvents(IEnumerable<ClickEvent> clickEvents);

        OperationResult<long> AddVisitEvent(VisitEvent visitEvents);
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
                return new OperationResult();
            }
            catch (Exception exp)
            {
                return new OperationResult(exp);
            }
        }
    }
}
