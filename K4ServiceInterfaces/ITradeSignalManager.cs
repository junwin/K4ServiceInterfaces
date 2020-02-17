
using System;
using System.Collections.Generic;
using System.Text;
using KaiTrade.Interfaces;

namespace K4ServiceInterface
{
    /// <summary>
    /// Interface of an object the allocates trade signals
    /// </summary>
    public interface ITradeSignalManager
    {
        /// <summary>
        /// Get a tradesignal using its guid/ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        KaiTrade.Interfaces.ITradeSignal GetSignal(string ID);

        /// <summary>
        /// create a new trade signal will register the signal in the managers list
        /// </summary>
        /// <param name="myName"></param>
        /// <returns></returns>
        KaiTrade.Interfaces.ITradeSignal CreateSignal(string myName);

   
        /// <summary>
        /// Register an existing signal with the manager - typically used for inbound signals
        /// </summary>
        /// <param name="signal"></param>
        void RegisterSignal(KaiTrade.Interfaces.ITradeSignal signal);

        /// <summary>
        /// Register an existing signal with the manager - typically used for inbound signals, and associate with the 
        /// strategy ID passed in
        /// </summary>
        /// <param name="signal"></param>
        /// <param name="strategyID"></param>
        void RegisterSignal(KaiTrade.Interfaces.ITradeSignal signal,string strategyID);

        /// <summary>
        /// Expose all registered trade signals
        /// </summary>
        List<KaiTrade.Interfaces.ITradeSignal> TradeSignals
        { get; set; }

        /// <summary>
        /// Compare two signals and return true if the data fields
        /// are the same - note the Identities are not included i.e. it only compares
        /// the data attributes - NOTE does not include dates
        /// </summary>
        /// <param name="s1">first signal to be compared</param>
        /// <param name="s2">second signal to be compared</param>
        /// <returns>true if all fields are the same</returns>
        bool IsSameSignalData(KaiTrade.Interfaces.ITradeSignal s1, KaiTrade.Interfaces.ITradeSignal s2);

        /// <summary>
        /// Set a property to be associated with some trade signal - based on the signal origin.
        /// </summary>
        /// <param name="signalName"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        void SetTradeSignalOriginProperty(string signalOrigin, string propertyName, string value);

        /// <summary>
        /// Get a property associated with some trade signal
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="signalOrigin"></param>
        /// <param name="propertyName"></param>
        /// <returns>true if found</returns>
        bool GetTradeSignalProperty(out string propertyValue, string signalOrigin, string propertyName);

        void AddReplaceTradeSystem(ITradeSystem tradeSystem);
    }
}

