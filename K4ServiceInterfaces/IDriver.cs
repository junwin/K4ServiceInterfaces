using log4net;
using KaiTrade.Interfaces;
using K4Channel;

/***************************************************************************
 *
 *      Copyright (c) 2009,2010,2011,2012 KaiTrade LLC (registered in Delaware)
 *                     All Rights Reserved Worldwide
 *
 * STRICTLY PROPRIETARY and CONFIDENTIAL
 *
 * WARNING:  This file is the confidential property of KaiTrade LLC For
 * use only by those with the express written permission and license from
 * KaiTrade LLC.  Unauthorized reproduction, distribution, use or disclosure
 * of this file or any program (or document) is prohibited.
 *
 ***************************************************************************/

using System.Collections.Generic;

namespace K4ServiceInterface
{
    public struct ProductUpdateType
    {
        public const string NEW = "new";
        public const string UPDATE = "update";      
    }
    /// <summary>
    /// Used to get bat updates
    /// </summary>
    /// <param name="bar"></param>
    public delegate void BarUpdate(string requestID, ITSItem[] bars);

    /// <summary>
    /// Used to get updates of a drivers connection status
    /// </summary>
    /// <param name="sender">the driver with updated connection status</param>
    /// <param name="sessionID">identity of the session or an empty string for single session drivers</param>
    /// <param name="statusUpdate">new stats</param>
    public delegate void ConnectionStatusUpdate(IDriver sender, string sessionID, IDriverConnectionStatus statusUpdate);

    public delegate void DOMUpdate(IProduct product, IDOMSlot[] slots);

    public delegate void NewMessage(IMessage myMessage);

    public delegate void NewStatusMessage(IMessage myMessage);

    /// <summary>
    /// Used to get price updates from the driver
    /// </summary>
    /// <param name="pxUpdate"></param>
    public delegate void PriceUpdate(IPXUpdate pxUpdate);

    /// <summary>
    /// Used to handle account additions and updates
    /// </summary>
    /// <param name="account"></param>
    public delegate void AccountUpdate(IAccount account);


    /// <summary>
    /// Used to get product updates
    /// </summary>
    /// <param name="updateType"></param>
    /// <param name="product"></param>
    public delegate void ProductUpdate(string updateType, IProduct product);

    public delegate void TradeSignalUpdate(ITSSet set, ITradeSignal tradeSignal);

    /// <summary>
    /// Results from a replace request - used for collaping replace queues
    /// </summary>
    public enum orderReplaceResult
    {
        /// <summary>
        /// Replace succeded
        /// </summary>
        success,

        /// <summary>
        /// Error during replace
        /// </summary>
        error,

        /// <summary>
        /// Could not replace at this time
        /// </summary>
        replacePending,

        /// <summary>
        /// Cannot cancel at this time
        /// </summary>
        cancelPending
    };
    /// <summary>
    /// Defines the coarse status of some driver attribute
    /// </summary>
    public enum StatusConditon { good, error, alert, none };

    /// <summary>
    /// Defines the interface a drive(broker connection) must impliment to be loaded and used from
    /// KaiTrade
    /// </summary>
    public interface IDriver
    {
        /// <summary>
        /// used to register delegates that will handle bar updates from the driver
        /// </summary>
        BarUpdate BarUpdate
        { get; set; }

        /// <summary>
        /// Used to register delegates for connection status updates
        /// </summary>
        ConnectionStatusUpdate ConnectionStatusUpdate
        { get; set; }

        DOMUpdate DOMUpdate
        { get; set; }

        /// <summary>
        ///  ID of the Adapter
        /// </summary>
        string ID
        {
            get;
            set;
        }

        string AppPath { get; set; }

        /// <summary>
        /// Driver def used to construct the driver
        /// </summary>
        IDriverDef DriverDef { get; set; }

        /// <summary>
        /// If set to true then run on the live market - not all drivers will obey this
        /// </summary>
        bool LiveMarket
        { get; set; }

        ILog Log
        { get; set; }

        NewMessage Message
        { get; set; }

        /// <summary>
        /// Get the name of the adapter
        /// </summary>
        /// <returns></returns>
        string Name
        {
            get;
        }

        /// <summary>
        /// Register a delgate to get price updates - this is complementary
        /// to register a specific publisher
        /// </summary>
        PriceUpdate PriceUpdate
        { get; set; }

        ProductUpdate ProductUpdate
        { get; set; }

        AccountUpdate AccountUpdate
        { get; set; }

        /// <summary>
        /// Get a list of sessions supported by the driver
        /// </summary>
        System.Collections.Generic.List<IDriverSession> Sessions
        {
            get;
        }

        /// <summary>
        /// Get the state data for the driver - usually passed to the driver
        /// at start up from the provisioning
        /// </summary>
        KaiTrade.Interfaces.Status Status { get; }

        NewStatusMessage StatusMessage
        { get; set; }

        /// <summary>
        /// Get/Set the tag - this is then returned on any message from the adapter
        /// </summary>
        string Tag
        {
            get;
            set;
        }

        TradeSignalUpdate TradeSignalUpdate
        { get; set; }

        /// <summary>
        /// Get a set of time series data - if supported
        /// </summary>
        /// <param name="myTSSet"></param>
        void DisconnectTSData(KaiTrade.Interfaces.ITSSet myTSSet);

        /// <summary>
        ///  Get the connection status of a driver session
        /// compliments the StatusRequest();
        /// </summary>
        /// <param name="sessions"> if null or empty the status of the primary session
        /// is returned</param>
        /// <returns>connection state for each session</returns>
        IDriverConnectionStatus[] GetConnectionStatus(IDriverSession[] sessions);

        /// <summary>
        /// Get a list of the available trade destinations for the cficode specified
        /// note that a market code represents some tradable market/exchange supported by the driver
        /// </summary>
        /// <param name="venueCode"> Cficode - the asset class that the market supports(futures, options, fx etc), if empty all markets </param>
        /// <returns>a list of trade destinations supported by the driver</returns>
        List<KaiTrade.Interfaces.IVenueTradeDestination> GetTradeDestinations(string cfiCode);

        /// <summary>
        /// Open prices and depth for the given product
        /// </summary>
        /// <param name="product">product to open prices on</param>
        /// <param name="depthLevels">Depth levels to request</param>
        /// <param name="requestID">identifier for this request</param>
        void OpenPrices(KaiTrade.Interfaces.IProduct product, int depthLevels, string requestID);

        /// <summary>
        /// Request any conditions that the driver supports- note that this
        /// is asyncronous the driver will add any conditions using the Facade
        /// </summary>
        void RequestConditions();

        /// <summary>
        /// Request the product details, get the driver to access the product and fill in
        /// product details in the kaitrade product object.
        /// Note that not all drivers support this and that the call may take some
        /// time to set the values i.e. it is not syncronous
        /// </summary>
        /// <param name="myProduct"></param>
        void RequestProductDetails(KaiTrade.Interfaces.IProduct myProduct);

        /// <summary>
        /// Will request any trade systems that the driver supports - note that this
        /// is asyncronous the driver will add any trading systems using the Facade
        /// </summary>
        void RequestTradeSystems();

        /// <summary>
        /// Request a set of time series data - if supported
        /// </summary>
        /// <param name="myTSSet">On or more TSSets</param>
        void RequestTSData(KaiTrade.Interfaces.ITSSet[] myTSSet);

        /// <summary>
        /// Send a message - the driver needs to process this message
        /// the Message usually wraps a FIX message for trading
        /// other drivers will use the relevant API
        /// </summary>
        /// <param name="myMsg"></param>
        void Send(IMessage myMsg);
        /// <summary>
        /// Set the parent of the adapter - this will be some
        /// type of toolkit. This enables a particular adapter to
        /// access that same set of facilities as the toolkit
        /// </summary>
        /// <param name="myParent"></param>
        void SetParent(IDriverManager myParent);

        /// <summary>
        /// Display or Hide any UI the driver has
        /// </summary>
        /// <param name="uiVisible">true => show UI, False => hide UI</param>
        void ShowUI(bool uiVisible);

        /// <summary>
        /// Start an adapter passing in any state needed - in this release
        /// the state is the working directory so an adapter can locate
        /// any other resources
        /// </summary>
        /// <param name="myState">string state data - this depends on the type od adapter</param>
        void Start(string myState);

        /// <summary>
        /// Request that all adapters report their status - the adapter returns the
        /// state through the status message that the client interface must support
        /// </summary>
        void StatusRequest();

        /// <summary>
        /// Stop the adapter
        /// </summary>
        void Stop();

        /// <summary>
        /// Unregister some subject
        /// </summary>
        /// <param name="myClient">subject that will be unregistered</param>
        void UnRegister(KaiTrade.Interfaces.IPublisher myPublisher);
    }
    /// <summary>
    /// Defines an interface to provide a snapshot of
    /// a drivers connection status - used to check that the
    /// driver is coected to the broker.
    /// </summary>
    public interface IDriverConnectionStatus
    {
        /// <summary>
        /// Status of the historic data connection
        /// </summary>
        StatusConditon HistoricData
        { get; set; }

        /// <summary>
        /// Status of the order routing connection
        /// </summary>
        StatusConditon OrderRouting
        { get; set; }

        /// <summary>
        /// Status of the prices connection
        /// </summary>
        StatusConditon Prices
        { get; set; }
    }
}