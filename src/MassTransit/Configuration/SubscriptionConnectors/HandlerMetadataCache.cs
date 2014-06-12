// Copyright 2007-2014 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.SubscriptionConnectors
{
    using System;
    using System.Threading;


    public class HandlerMetadataCache<T> :
        IHandlerMetadataCache<T>
        where T : class
    {
        readonly Lazy<HandlerMessageConnector<T>> _connector;

        public HandlerMetadataCache()
        {
            _connector = new Lazy<HandlerMessageConnector<T>>(() => new HandlerMessageConnector<T>());
        }

        public static MessageConnector Connector
        {
            get { return InstanceCache.Cached.Value.Connector; }
        }

        MessageConnector IHandlerMetadataCache<T>.Connector
        {
            get { return _connector.Value; }
        }


        static class InstanceCache
        {
            internal static readonly Lazy<IHandlerMetadataCache<T>> Cached = new Lazy<IHandlerMetadataCache<T>>(
                () => new HandlerMetadataCache<T>(), LazyThreadSafetyMode.PublicationOnly);
        }
    }
}