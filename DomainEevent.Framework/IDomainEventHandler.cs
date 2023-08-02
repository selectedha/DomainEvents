﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEevent.Framework
{
    public interface IDomainEventHandler<TDomainEvent> where TDomainEvent:IDomainEvent
    {
        Task Handle(TDomainEvent domainEvent);
    }
}
