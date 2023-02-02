// 
//  ____  _     __  __      _        _ 
// |  _ \| |__ |  \/  | ___| |_ __ _| |
// | | | | '_ \| |\/| |/ _ \ __/ _` | |
// | |_| | |_) | |  | |  __/ || (_| | |
// |____/|_.__/|_|  |_|\___|\__\__,_|_|
//
// Auto-generated from ITVDB on 2011-11-17 19:38:07Z.
// Please visit http://code.google.com/p/dblinq2007/ for more information.
//
namespace ITV.DataAccess.ITVDatabase
{
	using System;
	using System.ComponentModel;
	using System.Data;
#if MONO_STRICT
	using System.Data.Linq;
#else   // MONO_STRICT
	using DbLinq.Data.Linq;
	using DbLinq.Vendor;
#endif  // MONO_STRICT
	using System.Data.Linq.Mapping;
	using System.Diagnostics;
	
	
	public partial class ItVdB : DataContext
	{
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		#endregion
		
		
		public ItVdB(string connectionString) : 
				base(connectionString)
		{
			this.OnCreated();
		}
		
		public ItVdB(string connection, MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			this.OnCreated();
		}
		
		public ItVdB(IDbConnection connection, MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			this.OnCreated();
		}
		
		public Table<CNmCAteGory> CNmCAteGory
		{
			get
			{
				return this.GetTable<CNmCAteGory>();
			}
		}
		
		public Table<MsEvent> MsEvent
		{
			get
			{
				return this.GetTable<MsEvent>();
			}
		}
		
		public Table<MsGame> MsGame
		{
			get
			{
				return this.GetTable<MsGame>();
			}
		}
		
		public Table<MsGameBuffet> MsGameBuffet
		{
			get
			{
				return this.GetTable<MsGameBuffet>();
			}
		}
		
		public Table<MsGameBuffetDT> MsGameBuffetDT
		{
			get
			{
				return this.GetTable<MsGameBuffetDT>();
			}
		}
		
		public Table<MsHotelOutlet> MsHotelOutlet
		{
			get
			{
				return this.GetTable<MsHotelOutlet>();
			}
		}
		
		public Table<MsHotelOutletBooked> MsHotelOutletBooked
		{
			get
			{
				return this.GetTable<MsHotelOutletBooked>();
			}
		}
		
		public Table<MsHotelServices> MsHotelServices
		{
			get
			{
				return this.GetTable<MsHotelServices>();
			}
		}
		
		public Table<MsMenu> MsMenu
		{
			get
			{
				return this.GetTable<MsMenu>();
			}
		}
		
		public Table<MsMovie> MsMovie
		{
			get
			{
				return this.GetTable<MsMovie>();
			}
		}
		
		public Table<MsMovieBuffet> MsMovieBuffet
		{
			get
			{
				return this.GetTable<MsMovieBuffet>();
			}
		}
		
		public Table<MsMovieBuffetDT> MsMovieBuffetDT
		{
			get
			{
				return this.GetTable<MsMovieBuffetDT>();
			}
		}
		
		public Table<MsMovieCategory> MsMovieCategory
		{
			get
			{
				return this.GetTable<MsMovieCategory>();
			}
		}
		
		public Table<MsMovieLanguage> MsMovieLanguage
		{
			get
			{
				return this.GetTable<MsMovieLanguage>();
			}
		}
		
		public Table<MsMovieRating> MsMovieRating
		{
			get
			{
				return this.GetTable<MsMovieRating>();
			}
		}
		
		public Table<MsMusicAlbum> MsMusicAlbum
		{
			get
			{
				return this.GetTable<MsMusicAlbum>();
			}
		}
		
		public Table<MsMusicArtIsE> MsMusicArtIsE
		{
			get
			{
				return this.GetTable<MsMusicArtIsE>();
			}
		}
		
		public Table<MsMusicBuffet> MsMusicBuffet
		{
			get
			{
				return this.GetTable<MsMusicBuffet>();
			}
		}
		
		public Table<MsMusicBuffetDT> MsMusicBuffetDT
		{
			get
			{
				return this.GetTable<MsMusicBuffetDT>();
			}
		}
		
		public Table<MsMusicSong> MsMusicSong
		{
			get
			{
				return this.GetTable<MsMusicSong>();
			}
		}
		
		public Table<MsNews> MsNews
		{
			get
			{
				return this.GetTable<MsNews>();
			}
		}
		
		public Table<MsNewsCategory> MsNewsCategory
		{
			get
			{
				return this.GetTable<MsNewsCategory>();
			}
		}
		
		public Table<MsRadioCategory> MsRadioCategory
		{
			get
			{
				return this.GetTable<MsRadioCategory>();
			}
		}
		
		public Table<MsRadioStation> MsRadioStation
		{
			get
			{
				return this.GetTable<MsRadioStation>();
			}
		}
		
		public Table<MsRoomServiceCategory> MsRoomServiceCategory
		{
			get
			{
				return this.GetTable<MsRoomServiceCategory>();
			}
		}
		
		public Table<MsRoomServiceItem> MsRoomServiceItem
		{
			get
			{
				return this.GetTable<MsRoomServiceItem>();
			}
		}
		
		public Table<MsShoppingCategory> MsShoppingCategory
		{
			get
			{
				return this.GetTable<MsShoppingCategory>();
			}
		}
		
		public Table<MsShoppingItem> MsShoppingItem
		{
			get
			{
				return this.GetTable<MsShoppingItem>();
			}
		}
		
		public Table<MsSTB> MsSTB
		{
			get
			{
				return this.GetTable<MsSTB>();
			}
		}
		
		public Table<MsSTBmOdUlEVersion> MsSTBmOdUlEVersion
		{
			get
			{
				return this.GetTable<MsSTBmOdUlEVersion>();
			}
		}
		
		public Table<MsTourCategory> MsTourCategory
		{
			get
			{
				return this.GetTable<MsTourCategory>();
			}
		}
		
		public Table<MsTourDetail> MsTourDetail
		{
			get
			{
				return this.GetTable<MsTourDetail>();
			}
		}
		
		public Table<MsTvCAteGory> MsTvCAteGory
		{
			get
			{
				return this.GetTable<MsTvCAteGory>();
			}
		}
		
		public Table<MsTvCHanNeL> MsTvCHanNeL
		{
			get
			{
				return this.GetTable<MsTvCHanNeL>();
			}
		}
		
		public Table<MsWeatherCountry> MsWeatherCountry
		{
			get
			{
				return this.GetTable<MsWeatherCountry>();
			}
		}
		
		public Table<MsWeatherLocation> MsWeatherLocation
		{
			get
			{
				return this.GetTable<MsWeatherLocation>();
			}
		}
		
		public Table<MsWeatherRegion> MsWeatherRegion
		{
			get
			{
				return this.GetTable<MsWeatherRegion>();
			}
		}
		
		public Table<TRBillingRoom> TRBillingRoom
		{
			get
			{
				return this.GetTable<TRBillingRoom>();
			}
		}
		
		public Table<UsRMSRoles> UsRMSRoles
		{
			get
			{
				return this.GetTable<UsRMSRoles>();
			}
		}
		
		public Table<UsRMSUsers> UsRMSUsers
		{
			get
			{
				return this.GetTable<UsRMSUsers>();
			}
		}
		
		public Table<UsRuSERSInRoles> UsRuSERSInRoles
		{
			get
			{
				return this.GetTable<UsRuSERSInRoles>();
			}
		}
	}
	
	#region Start MONO_STRICT
#if MONO_STRICT

	public partial class ItVdB
	{
		
		public ItVdB(IDbConnection connection) : 
				base(connection)
		{
			this.OnCreated();
		}
	}
	#region End MONO_STRICT
	#endregion
#else     // MONO_STRICT
	
	public partial class ItVdB
	{
		
		public ItVdB(IDbConnection connection) : 
				base(connection, new DbLinq.MySql.MySqlVendor())
		{
			this.OnCreated();
		}
		
		public ItVdB(IDbConnection connection, IVendor sqlDialect) : 
				base(connection, sqlDialect)
		{
			this.OnCreated();
		}
		
		public ItVdB(IDbConnection connection, MappingSource mappingSource, IVendor sqlDialect) : 
				base(connection, mappingSource, sqlDialect)
		{
			this.OnCreated();
		}
	}
	#region End Not MONO_STRICT
	#endregion
#endif     // MONO_STRICT
	#endregion
	
	[Table(Name="ITVDB.CNM_Category")]
	public partial class CNmCAteGory : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private uint _categoryID;
		
		private System.Nullable<sbyte> _categoryIsActive;
		
		private System.Nullable<sbyte> _categoryIsParentalLock;
		
		private string _categoryName;
		
		private string _categoryPoster;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCategoryIDChanged();
		
		partial void OnCategoryIDChanging(uint value);
		
		partial void OnCategoryIsActiveChanged();
		
		partial void OnCategoryIsActiveChanging(System.Nullable<sbyte> value);
		
		partial void OnCategoryIsParentalLockChanged();
		
		partial void OnCategoryIsParentalLockChanging(System.Nullable<sbyte> value);
		
		partial void OnCategoryNameChanged();
		
		partial void OnCategoryNameChanging(string value);
		
		partial void OnCategoryPosterChanged();
		
		partial void OnCategoryPosterChanging(string value);
		#endregion
		
		
		public CNmCAteGory()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_categoryID", Name="category_id", DbType="int unsigned", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public uint CategoryID
		{
			get
			{
				return this._categoryID;
			}
			set
			{
				if ((_categoryID != value))
				{
					this.OnCategoryIDChanging(value);
					this.SendPropertyChanging();
					this._categoryID = value;
					this.SendPropertyChanged("CategoryID");
					this.OnCategoryIDChanged();
				}
			}
		}
		
		[Column(Storage="_categoryIsActive", Name="category_isActive", DbType="tinyint(1)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<sbyte> CategoryIsActive
		{
			get
			{
				return this._categoryIsActive;
			}
			set
			{
				if ((_categoryIsActive != value))
				{
					this.OnCategoryIsActiveChanging(value);
					this.SendPropertyChanging();
					this._categoryIsActive = value;
					this.SendPropertyChanged("CategoryIsActive");
					this.OnCategoryIsActiveChanged();
				}
			}
		}
		
		[Column(Storage="_categoryIsParentalLock", Name="category_isParentalLock", DbType="tinyint(1)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<sbyte> CategoryIsParentalLock
		{
			get
			{
				return this._categoryIsParentalLock;
			}
			set
			{
				if ((_categoryIsParentalLock != value))
				{
					this.OnCategoryIsParentalLockChanging(value);
					this.SendPropertyChanging();
					this._categoryIsParentalLock = value;
					this.SendPropertyChanged("CategoryIsParentalLock");
					this.OnCategoryIsParentalLockChanged();
				}
			}
		}
		
		[Column(Storage="_categoryName", Name="category_name", DbType="varchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CategoryName
		{
			get
			{
				return this._categoryName;
			}
			set
			{
				if (((_categoryName == value) 
							== false))
				{
					this.OnCategoryNameChanging(value);
					this.SendPropertyChanging();
					this._categoryName = value;
					this.SendPropertyChanged("CategoryName");
					this.OnCategoryNameChanged();
				}
			}
		}
		
		[Column(Storage="_categoryPoster", Name="category_poster", DbType="varchar(255)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CategoryPoster
		{
			get
			{
				return this._categoryPoster;
			}
			set
			{
				if (((_categoryPoster == value) 
							== false))
				{
					this.OnCategoryPosterChanging(value);
					this.SendPropertyChanging();
					this._categoryPoster = value;
					this.SendPropertyChanged("CategoryPoster");
					this.OnCategoryPosterChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsEvent")]
	public partial class MsEvent : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _eventDesc;
		
		private long _eventID;
		
		private string _eventName;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		private string _videoFile;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnEventDescChanged();
		
		partial void OnEventDescChanging(string value);
		
		partial void OnEventIDChanged();
		
		partial void OnEventIDChanging(long value);
		
		partial void OnEventNameChanged();
		
		partial void OnEventNameChanging(string value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		
		partial void OnVideoFileChanged();
		
		partial void OnVideoFileChanging(string value);
		#endregion
		
		
		public MsEvent()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_eventDesc", Name="EventDesc", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string EventDesc
		{
			get
			{
				return this._eventDesc;
			}
			set
			{
				if (((_eventDesc == value) 
							== false))
				{
					this.OnEventDescChanging(value);
					this.SendPropertyChanging();
					this._eventDesc = value;
					this.SendPropertyChanged("EventDesc");
					this.OnEventDescChanged();
				}
			}
		}
		
		[Column(Storage="_eventID", Name="EventID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long EventID
		{
			get
			{
				return this._eventID;
			}
			set
			{
				if ((_eventID != value))
				{
					this.OnEventIDChanging(value);
					this.SendPropertyChanging();
					this._eventID = value;
					this.SendPropertyChanged("EventID");
					this.OnEventIDChanged();
				}
			}
		}
		
		[Column(Storage="_eventName", Name="EventName", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string EventName
		{
			get
			{
				return this._eventName;
			}
			set
			{
				if (((_eventName == value) 
							== false))
				{
					this.OnEventNameChanging(value);
					this.SendPropertyChanging();
					this._eventName = value;
					this.SendPropertyChanged("EventName");
					this.OnEventNameChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		[Column(Storage="_videoFile", Name="VideoFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string VideoFile
		{
			get
			{
				return this._videoFile;
			}
			set
			{
				if (((_videoFile == value) 
							== false))
				{
					this.OnVideoFileChanging(value);
					this.SendPropertyChanging();
					this._videoFile = value;
					this.SendPropertyChanged("VideoFile");
					this.OnVideoFileChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsGame")]
	public partial class MsGame : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _gameDesc;
		
		private long _gameID;
		
		private string _gameName;
		
		private string _gamePath;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnGameDescChanged();
		
		partial void OnGameDescChanging(string value);
		
		partial void OnGameIDChanged();
		
		partial void OnGameIDChanging(long value);
		
		partial void OnGameNameChanged();
		
		partial void OnGameNameChanging(string value);
		
		partial void OnGamePathChanged();
		
		partial void OnGamePathChanging(string value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsGame()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_gameDesc", Name="GameDesc", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string GameDesc
		{
			get
			{
				return this._gameDesc;
			}
			set
			{
				if (((_gameDesc == value) 
							== false))
				{
					this.OnGameDescChanging(value);
					this.SendPropertyChanging();
					this._gameDesc = value;
					this.SendPropertyChanged("GameDesc");
					this.OnGameDescChanged();
				}
			}
		}
		
		[Column(Storage="_gameID", Name="GameID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long GameID
		{
			get
			{
				return this._gameID;
			}
			set
			{
				if ((_gameID != value))
				{
					this.OnGameIDChanging(value);
					this.SendPropertyChanging();
					this._gameID = value;
					this.SendPropertyChanged("GameID");
					this.OnGameIDChanged();
				}
			}
		}
		
		[Column(Storage="_gameName", Name="GameName", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string GameName
		{
			get
			{
				return this._gameName;
			}
			set
			{
				if (((_gameName == value) 
							== false))
				{
					this.OnGameNameChanging(value);
					this.SendPropertyChanging();
					this._gameName = value;
					this.SendPropertyChanged("GameName");
					this.OnGameNameChanged();
				}
			}
		}
		
		[Column(Storage="_gamePath", Name="GamePath", DbType="varchar(200)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string GamePath
		{
			get
			{
				return this._gamePath;
			}
			set
			{
				if (((_gamePath == value) 
							== false))
				{
					this.OnGamePathChanging(value);
					this.SendPropertyChanging();
					this._gamePath = value;
					this.SendPropertyChanged("GamePath");
					this.OnGamePathChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsGameBuffet")]
	public partial class MsGameBuffet : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _gameBuffetDesc;
		
		private long _gameBuffetID;
		
		private string _gameBuffetName;
		
		private string _gameBuffetType;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnGameBuffetDescChanged();
		
		partial void OnGameBuffetDescChanging(string value);
		
		partial void OnGameBuffetIDChanged();
		
		partial void OnGameBuffetIDChanging(long value);
		
		partial void OnGameBuffetNameChanged();
		
		partial void OnGameBuffetNameChanging(string value);
		
		partial void OnGameBuffetTypeChanged();
		
		partial void OnGameBuffetTypeChanging(string value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsGameBuffet()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_gameBuffetDesc", Name="GameBuffetDesc", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string GameBuffetDesc
		{
			get
			{
				return this._gameBuffetDesc;
			}
			set
			{
				if (((_gameBuffetDesc == value) 
							== false))
				{
					this.OnGameBuffetDescChanging(value);
					this.SendPropertyChanging();
					this._gameBuffetDesc = value;
					this.SendPropertyChanged("GameBuffetDesc");
					this.OnGameBuffetDescChanged();
				}
			}
		}
		
		[Column(Storage="_gameBuffetID", Name="GameBuffetID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long GameBuffetID
		{
			get
			{
				return this._gameBuffetID;
			}
			set
			{
				if ((_gameBuffetID != value))
				{
					this.OnGameBuffetIDChanging(value);
					this.SendPropertyChanging();
					this._gameBuffetID = value;
					this.SendPropertyChanged("GameBuffetID");
					this.OnGameBuffetIDChanged();
				}
			}
		}
		
		[Column(Storage="_gameBuffetName", Name="GameBuffetName", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string GameBuffetName
		{
			get
			{
				return this._gameBuffetName;
			}
			set
			{
				if (((_gameBuffetName == value) 
							== false))
				{
					this.OnGameBuffetNameChanging(value);
					this.SendPropertyChanging();
					this._gameBuffetName = value;
					this.SendPropertyChanged("GameBuffetName");
					this.OnGameBuffetNameChanged();
				}
			}
		}
		
		[Column(Storage="_gameBuffetType", Name="GameBuffetType", DbType="char(1)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string GameBuffetType
		{
			get
			{
				return this._gameBuffetType;
			}
			set
			{
				if (((_gameBuffetType == value) 
							== false))
				{
					this.OnGameBuffetTypeChanging(value);
					this.SendPropertyChanging();
					this._gameBuffetType = value;
					this.SendPropertyChanged("GameBuffetType");
					this.OnGameBuffetTypeChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsGameBuffetDt")]
	public partial class MsGameBuffetDT : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private long _gameBuffetDtID;
		
		private long _gameBuffetID;
		
		private long _gameID;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnGameBuffetDtIDChanged();
		
		partial void OnGameBuffetDtIDChanging(long value);
		
		partial void OnGameBuffetIDChanged();
		
		partial void OnGameBuffetIDChanging(long value);
		
		partial void OnGameIDChanged();
		
		partial void OnGameIDChanging(long value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsGameBuffetDT()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_gameBuffetDtID", Name="GameBuffetDtID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long GameBuffetDtID
		{
			get
			{
				return this._gameBuffetDtID;
			}
			set
			{
				if ((_gameBuffetDtID != value))
				{
					this.OnGameBuffetDtIDChanging(value);
					this.SendPropertyChanging();
					this._gameBuffetDtID = value;
					this.SendPropertyChanged("GameBuffetDtID");
					this.OnGameBuffetDtIDChanged();
				}
			}
		}
		
		[Column(Storage="_gameBuffetID", Name="GameBuffetID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long GameBuffetID
		{
			get
			{
				return this._gameBuffetID;
			}
			set
			{
				if ((_gameBuffetID != value))
				{
					this.OnGameBuffetIDChanging(value);
					this.SendPropertyChanging();
					this._gameBuffetID = value;
					this.SendPropertyChanged("GameBuffetID");
					this.OnGameBuffetIDChanged();
				}
			}
		}
		
		[Column(Storage="_gameID", Name="GameID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long GameID
		{
			get
			{
				return this._gameID;
			}
			set
			{
				if ((_gameID != value))
				{
					this.OnGameIDChanging(value);
					this.SendPropertyChanging();
					this._gameID = value;
					this.SendPropertyChanged("GameID");
					this.OnGameIDChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsHotelOutlet")]
	public partial class MsHotelOutlet : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private System.Nullable<sbyte> _hotelOutletBooked;
		
		private string _hotelOutletDesc;
		
		private long _hotelOutletID;
		
		private string _hotelOutletName;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private int _rowStatus;
		
		private System.Nullable<System.DateTime> _timestamp;
		
		private string _videoFile;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnHotelOutletBookedChanged();
		
		partial void OnHotelOutletBookedChanging(System.Nullable<sbyte> value);
		
		partial void OnHotelOutletDescChanged();
		
		partial void OnHotelOutletDescChanging(string value);
		
		partial void OnHotelOutletIDChanged();
		
		partial void OnHotelOutletIDChanging(long value);
		
		partial void OnHotelOutletNameChanged();
		
		partial void OnHotelOutletNameChanging(string value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.Nullable<System.DateTime> value);
		
		partial void OnVideoFileChanged();
		
		partial void OnVideoFileChanging(string value);
		#endregion
		
		
		public MsHotelOutlet()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_hotelOutletBooked", Name="HotelOutletBooked", DbType="tinyint(4)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<sbyte> HotelOutletBooked
		{
			get
			{
				return this._hotelOutletBooked;
			}
			set
			{
				if ((_hotelOutletBooked != value))
				{
					this.OnHotelOutletBookedChanging(value);
					this.SendPropertyChanging();
					this._hotelOutletBooked = value;
					this.SendPropertyChanged("HotelOutletBooked");
					this.OnHotelOutletBookedChanged();
				}
			}
		}
		
		[Column(Storage="_hotelOutletDesc", Name="HotelOutletDesc", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string HotelOutletDesc
		{
			get
			{
				return this._hotelOutletDesc;
			}
			set
			{
				if (((_hotelOutletDesc == value) 
							== false))
				{
					this.OnHotelOutletDescChanging(value);
					this.SendPropertyChanging();
					this._hotelOutletDesc = value;
					this.SendPropertyChanged("HotelOutletDesc");
					this.OnHotelOutletDescChanged();
				}
			}
		}
		
		[Column(Storage="_hotelOutletID", Name="HotelOutletID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long HotelOutletID
		{
			get
			{
				return this._hotelOutletID;
			}
			set
			{
				if ((_hotelOutletID != value))
				{
					this.OnHotelOutletIDChanging(value);
					this.SendPropertyChanging();
					this._hotelOutletID = value;
					this.SendPropertyChanged("HotelOutletID");
					this.OnHotelOutletIDChanged();
				}
			}
		}
		
		[Column(Storage="_hotelOutletName", Name="HotelOutletName", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string HotelOutletName
		{
			get
			{
				return this._hotelOutletName;
			}
			set
			{
				if (((_hotelOutletName == value) 
							== false))
				{
					this.OnHotelOutletNameChanging(value);
					this.SendPropertyChanging();
					this._hotelOutletName = value;
					this.SendPropertyChanged("HotelOutletName");
					this.OnHotelOutletNameChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		[Column(Storage="_videoFile", Name="VideoFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string VideoFile
		{
			get
			{
				return this._videoFile;
			}
			set
			{
				if (((_videoFile == value) 
							== false))
				{
					this.OnVideoFileChanging(value);
					this.SendPropertyChanging();
					this._videoFile = value;
					this.SendPropertyChanged("VideoFile");
					this.OnVideoFileChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsHotelOutletBooked")]
	public partial class MsHotelOutletBooked : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private System.Nullable<System.DateTime> _bookedDate;
		
		private string _bookedEvent;
		
		private long _bookedID;
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _guestBooked;
		
		private long _hotelOutletID;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private System.Nullable<int> _rowStatus;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnBookedDateChanged();
		
		partial void OnBookedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnBookedEventChanged();
		
		partial void OnBookedEventChanging(string value);
		
		partial void OnBookedIDChanged();
		
		partial void OnBookedIDChanging(long value);
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnGuestBookedChanged();
		
		partial void OnGuestBookedChanging(string value);
		
		partial void OnHotelOutletIDChanged();
		
		partial void OnHotelOutletIDChanging(long value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(System.Nullable<int> value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsHotelOutletBooked()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_bookedDate", Name="BookedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> BookedDate
		{
			get
			{
				return this._bookedDate;
			}
			set
			{
				if ((_bookedDate != value))
				{
					this.OnBookedDateChanging(value);
					this.SendPropertyChanging();
					this._bookedDate = value;
					this.SendPropertyChanged("BookedDate");
					this.OnBookedDateChanged();
				}
			}
		}
		
		[Column(Storage="_bookedEvent", Name="BookedEvent", DbType="varchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string BookedEvent
		{
			get
			{
				return this._bookedEvent;
			}
			set
			{
				if (((_bookedEvent == value) 
							== false))
				{
					this.OnBookedEventChanging(value);
					this.SendPropertyChanging();
					this._bookedEvent = value;
					this.SendPropertyChanged("BookedEvent");
					this.OnBookedEventChanged();
				}
			}
		}
		
		[Column(Storage="_bookedID", Name="BookedID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long BookedID
		{
			get
			{
				return this._bookedID;
			}
			set
			{
				if ((_bookedID != value))
				{
					this.OnBookedIDChanging(value);
					this.SendPropertyChanging();
					this._bookedID = value;
					this.SendPropertyChanged("BookedID");
					this.OnBookedIDChanged();
				}
			}
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_guestBooked", Name="GuestBooked", DbType="varchar(100)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string GuestBooked
		{
			get
			{
				return this._guestBooked;
			}
			set
			{
				if (((_guestBooked == value) 
							== false))
				{
					this.OnGuestBookedChanging(value);
					this.SendPropertyChanging();
					this._guestBooked = value;
					this.SendPropertyChanged("GuestBooked");
					this.OnGuestBookedChanged();
				}
			}
		}
		
		[Column(Storage="_hotelOutletID", Name="HotelOutletID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long HotelOutletID
		{
			get
			{
				return this._hotelOutletID;
			}
			set
			{
				if ((_hotelOutletID != value))
				{
					this.OnHotelOutletIDChanging(value);
					this.SendPropertyChanging();
					this._hotelOutletID = value;
					this.SendPropertyChanged("HotelOutletID");
					this.OnHotelOutletIDChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsHotelServices")]
	public partial class MsHotelServices : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _hotelServicesDesc;
		
		private long _hotelServicesID;
		
		private string _hotelServicesName;
		
		private string _hotelServicesTitle;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		private string _videoFile;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnHotelServicesDescChanged();
		
		partial void OnHotelServicesDescChanging(string value);
		
		partial void OnHotelServicesIDChanged();
		
		partial void OnHotelServicesIDChanging(long value);
		
		partial void OnHotelServicesNameChanged();
		
		partial void OnHotelServicesNameChanging(string value);
		
		partial void OnHotelServicesTitleChanged();
		
		partial void OnHotelServicesTitleChanging(string value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		
		partial void OnVideoFileChanged();
		
		partial void OnVideoFileChanging(string value);
		#endregion
		
		
		public MsHotelServices()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_hotelServicesDesc", Name="HotelServicesDesc", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string HotelServicesDesc
		{
			get
			{
				return this._hotelServicesDesc;
			}
			set
			{
				if (((_hotelServicesDesc == value) 
							== false))
				{
					this.OnHotelServicesDescChanging(value);
					this.SendPropertyChanging();
					this._hotelServicesDesc = value;
					this.SendPropertyChanged("HotelServicesDesc");
					this.OnHotelServicesDescChanged();
				}
			}
		}
		
		[Column(Storage="_hotelServicesID", Name="HotelServicesID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long HotelServicesID
		{
			get
			{
				return this._hotelServicesID;
			}
			set
			{
				if ((_hotelServicesID != value))
				{
					this.OnHotelServicesIDChanging(value);
					this.SendPropertyChanging();
					this._hotelServicesID = value;
					this.SendPropertyChanged("HotelServicesID");
					this.OnHotelServicesIDChanged();
				}
			}
		}
		
		[Column(Storage="_hotelServicesName", Name="HotelServicesName", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string HotelServicesName
		{
			get
			{
				return this._hotelServicesName;
			}
			set
			{
				if (((_hotelServicesName == value) 
							== false))
				{
					this.OnHotelServicesNameChanging(value);
					this.SendPropertyChanging();
					this._hotelServicesName = value;
					this.SendPropertyChanged("HotelServicesName");
					this.OnHotelServicesNameChanged();
				}
			}
		}
		
		[Column(Storage="_hotelServicesTitle", Name="HotelServicesTitle", DbType="varchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string HotelServicesTitle
		{
			get
			{
				return this._hotelServicesTitle;
			}
			set
			{
				if (((_hotelServicesTitle == value) 
							== false))
				{
					this.OnHotelServicesTitleChanging(value);
					this.SendPropertyChanging();
					this._hotelServicesTitle = value;
					this.SendPropertyChanged("HotelServicesTitle");
					this.OnHotelServicesTitleChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		[Column(Storage="_videoFile", Name="VideoFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string VideoFile
		{
			get
			{
				return this._videoFile;
			}
			set
			{
				if (((_videoFile == value) 
							== false))
				{
					this.OnVideoFileChanging(value);
					this.SendPropertyChanging();
					this._videoFile = value;
					this.SendPropertyChanged("VideoFile");
					this.OnVideoFileChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsMenu")]
	public partial class MsMenu : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _imageUrl;
		
		private System.Nullable<sbyte> _indent;
		
		private System.Nullable<bool> _isActive;
		
		private string _menuIcon;
		
		private short _menuID;
		
		private string _moduleID;
		
		private string _navigateUrl;
		
		private string _onSelectedImageUrl;
		
		private System.Nullable<short> _parentID;
		
		private System.Nullable<int> _priority;
		
		private System.Nullable<bool> _showInQuickLaunch;
		
		private string _siteMapUrl;
		
		private string _text;
		
		private string _value;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnImageUrlChanged();
		
		partial void OnImageUrlChanging(string value);
		
		partial void OnIndentChanged();
		
		partial void OnIndentChanging(System.Nullable<sbyte> value);
		
		partial void OnIsActiveChanged();
		
		partial void OnIsActiveChanging(System.Nullable<bool> value);
		
		partial void OnMenuIconChanged();
		
		partial void OnMenuIconChanging(string value);
		
		partial void OnMenuIDChanged();
		
		partial void OnMenuIDChanging(short value);
		
		partial void OnModuleIDChanged();
		
		partial void OnModuleIDChanging(string value);
		
		partial void OnNavigateUrlChanged();
		
		partial void OnNavigateUrlChanging(string value);
		
		partial void OnOnSelectedImageUrlChanged();
		
		partial void OnOnSelectedImageUrlChanging(string value);
		
		partial void OnParentIDChanged();
		
		partial void OnParentIDChanging(System.Nullable<short> value);
		
		partial void OnPriorityChanged();
		
		partial void OnPriorityChanging(System.Nullable<int> value);
		
		partial void OnShowInQuickLaunchChanged();
		
		partial void OnShowInQuickLaunchChanging(System.Nullable<bool> value);
		
		partial void OnSiteMapUrlChanged();
		
		partial void OnSiteMapUrlChanging(string value);
		
		partial void OnTextChanged();
		
		partial void OnTextChanging(string value);
		
		partial void OnValueChanged();
		
		partial void OnValueChanging(string value);
		#endregion
		
		
		public MsMenu()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_imageUrl", Name="ImageURL", DbType="varchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageUrl
		{
			get
			{
				return this._imageUrl;
			}
			set
			{
				if (((_imageUrl == value) 
							== false))
				{
					this.OnImageUrlChanging(value);
					this.SendPropertyChanging();
					this._imageUrl = value;
					this.SendPropertyChanged("ImageUrl");
					this.OnImageUrlChanged();
				}
			}
		}
		
		[Column(Storage="_indent", Name="Indent", DbType="tinyint(4)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<sbyte> Indent
		{
			get
			{
				return this._indent;
			}
			set
			{
				if ((_indent != value))
				{
					this.OnIndentChanging(value);
					this.SendPropertyChanging();
					this._indent = value;
					this.SendPropertyChanged("Indent");
					this.OnIndentChanged();
				}
			}
		}
		
		[Column(Storage="_isActive", Name="IsActive", DbType="bit(1)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<bool> IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if ((_isActive != value))
				{
					this.OnIsActiveChanging(value);
					this.SendPropertyChanging();
					this._isActive = value;
					this.SendPropertyChanged("IsActive");
					this.OnIsActiveChanged();
				}
			}
		}
		
		[Column(Storage="_menuIcon", Name="MenuIcon", DbType="varchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MenuIcon
		{
			get
			{
				return this._menuIcon;
			}
			set
			{
				if (((_menuIcon == value) 
							== false))
				{
					this.OnMenuIconChanging(value);
					this.SendPropertyChanging();
					this._menuIcon = value;
					this.SendPropertyChanged("MenuIcon");
					this.OnMenuIconChanged();
				}
			}
		}
		
		[Column(Storage="_menuID", Name="MenuID", DbType="smallint(6)", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public short MenuID
		{
			get
			{
				return this._menuID;
			}
			set
			{
				if ((_menuID != value))
				{
					this.OnMenuIDChanging(value);
					this.SendPropertyChanging();
					this._menuID = value;
					this.SendPropertyChanged("MenuID");
					this.OnMenuIDChanged();
				}
			}
		}
		
		[Column(Storage="_moduleID", Name="ModuleID", DbType="varchar(5)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModuleID
		{
			get
			{
				return this._moduleID;
			}
			set
			{
				if (((_moduleID == value) 
							== false))
				{
					this.OnModuleIDChanging(value);
					this.SendPropertyChanging();
					this._moduleID = value;
					this.SendPropertyChanged("ModuleID");
					this.OnModuleIDChanged();
				}
			}
		}
		
		[Column(Storage="_navigateUrl", Name="NavigateURL", DbType="varchar(500)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string NavigateUrl
		{
			get
			{
				return this._navigateUrl;
			}
			set
			{
				if (((_navigateUrl == value) 
							== false))
				{
					this.OnNavigateUrlChanging(value);
					this.SendPropertyChanging();
					this._navigateUrl = value;
					this.SendPropertyChanged("NavigateUrl");
					this.OnNavigateUrlChanged();
				}
			}
		}
		
		[Column(Storage="_onSelectedImageUrl", Name="OnSelectedImageURL", DbType="varchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string OnSelectedImageUrl
		{
			get
			{
				return this._onSelectedImageUrl;
			}
			set
			{
				if (((_onSelectedImageUrl == value) 
							== false))
				{
					this.OnOnSelectedImageUrlChanging(value);
					this.SendPropertyChanging();
					this._onSelectedImageUrl = value;
					this.SendPropertyChanged("OnSelectedImageUrl");
					this.OnOnSelectedImageUrlChanged();
				}
			}
		}
		
		[Column(Storage="_parentID", Name="ParentID", DbType="smallint(6)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<short> ParentID
		{
			get
			{
				return this._parentID;
			}
			set
			{
				if ((_parentID != value))
				{
					this.OnParentIDChanging(value);
					this.SendPropertyChanging();
					this._parentID = value;
					this.SendPropertyChanged("ParentID");
					this.OnParentIDChanged();
				}
			}
		}
		
		[Column(Storage="_priority", Name="Priority", DbType="int", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> Priority
		{
			get
			{
				return this._priority;
			}
			set
			{
				if ((_priority != value))
				{
					this.OnPriorityChanging(value);
					this.SendPropertyChanging();
					this._priority = value;
					this.SendPropertyChanged("Priority");
					this.OnPriorityChanged();
				}
			}
		}
		
		[Column(Storage="_showInQuickLaunch", Name="ShowInQuickLaunch", DbType="bit(1)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<bool> ShowInQuickLaunch
		{
			get
			{
				return this._showInQuickLaunch;
			}
			set
			{
				if ((_showInQuickLaunch != value))
				{
					this.OnShowInQuickLaunchChanging(value);
					this.SendPropertyChanging();
					this._showInQuickLaunch = value;
					this.SendPropertyChanged("ShowInQuickLaunch");
					this.OnShowInQuickLaunchChanged();
				}
			}
		}
		
		[Column(Storage="_siteMapUrl", Name="SiteMapURL", DbType="varchar(500)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string SiteMapUrl
		{
			get
			{
				return this._siteMapUrl;
			}
			set
			{
				if (((_siteMapUrl == value) 
							== false))
				{
					this.OnSiteMapUrlChanging(value);
					this.SendPropertyChanging();
					this._siteMapUrl = value;
					this.SendPropertyChanged("SiteMapUrl");
					this.OnSiteMapUrlChanged();
				}
			}
		}
		
		[Column(Storage="_text", Name="Text", DbType="varchar(30)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if (((_text == value) 
							== false))
				{
					this.OnTextChanging(value);
					this.SendPropertyChanging();
					this._text = value;
					this.SendPropertyChanged("Text");
					this.OnTextChanged();
				}
			}
		}
		
		[Column(Storage="_value", Name="Value", DbType="varchar(30)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (((_value == value) 
							== false))
				{
					this.OnValueChanging(value);
					this.SendPropertyChanging();
					this._value = value;
					this.SendPropertyChanged("Value");
					this.OnValueChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsMovie")]
	public partial class MsMovie : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private long _categoryID;
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _image;
		
		private long _languageID;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private string _movieDuration;
		
		private long _movieID;
		
		private string _movieName;
		
		private string _movieSynopsis;
		
		private string _movieYear;
		
		private long _ratingID;
		
		private System.Nullable<int> _rowStatus;
		
		private System.DateTime _timestamp;
		
		private string _trailerUrlS;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCategoryIDChanged();
		
		partial void OnCategoryIDChanging(long value);
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImageChanged();
		
		partial void OnImageChanging(string value);
		
		partial void OnLanguageIDChanged();
		
		partial void OnLanguageIDChanging(long value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnMovieDurationChanged();
		
		partial void OnMovieDurationChanging(string value);
		
		partial void OnMovieIDChanged();
		
		partial void OnMovieIDChanging(long value);
		
		partial void OnMovieNameChanged();
		
		partial void OnMovieNameChanging(string value);
		
		partial void OnMovieSynopsisChanged();
		
		partial void OnMovieSynopsisChanging(string value);
		
		partial void OnMovieYearChanged();
		
		partial void OnMovieYearChanging(string value);
		
		partial void OnRatingIDChanged();
		
		partial void OnRatingIDChanging(long value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(System.Nullable<int> value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		
		partial void OnTrailerUrlSChanged();
		
		partial void OnTrailerUrlSChanging(string value);
		#endregion
		
		
		public MsMovie()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_categoryID", Name="CategoryID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long CategoryID
		{
			get
			{
				return this._categoryID;
			}
			set
			{
				if ((_categoryID != value))
				{
					this.OnCategoryIDChanging(value);
					this.SendPropertyChanging();
					this._categoryID = value;
					this.SendPropertyChanged("CategoryID");
					this.OnCategoryIDChanged();
				}
			}
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_image", Name="Image", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Image
		{
			get
			{
				return this._image;
			}
			set
			{
				if (((_image == value) 
							== false))
				{
					this.OnImageChanging(value);
					this.SendPropertyChanging();
					this._image = value;
					this.SendPropertyChanged("Image");
					this.OnImageChanged();
				}
			}
		}
		
		[Column(Storage="_languageID", Name="LanguageID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long LanguageID
		{
			get
			{
				return this._languageID;
			}
			set
			{
				if ((_languageID != value))
				{
					this.OnLanguageIDChanging(value);
					this.SendPropertyChanging();
					this._languageID = value;
					this.SendPropertyChanged("LanguageID");
					this.OnLanguageIDChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_movieDuration", Name="MovieDuration", DbType="varchar(10)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MovieDuration
		{
			get
			{
				return this._movieDuration;
			}
			set
			{
				if (((_movieDuration == value) 
							== false))
				{
					this.OnMovieDurationChanging(value);
					this.SendPropertyChanging();
					this._movieDuration = value;
					this.SendPropertyChanged("MovieDuration");
					this.OnMovieDurationChanged();
				}
			}
		}
		
		[Column(Storage="_movieID", Name="MovieID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long MovieID
		{
			get
			{
				return this._movieID;
			}
			set
			{
				if ((_movieID != value))
				{
					this.OnMovieIDChanging(value);
					this.SendPropertyChanging();
					this._movieID = value;
					this.SendPropertyChanged("MovieID");
					this.OnMovieIDChanged();
				}
			}
		}
		
		[Column(Storage="_movieName", Name="MovieName", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MovieName
		{
			get
			{
				return this._movieName;
			}
			set
			{
				if (((_movieName == value) 
							== false))
				{
					this.OnMovieNameChanging(value);
					this.SendPropertyChanging();
					this._movieName = value;
					this.SendPropertyChanged("MovieName");
					this.OnMovieNameChanged();
				}
			}
		}
		
		[Column(Storage="_movieSynopsis", Name="MovieSynopsis", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MovieSynopsis
		{
			get
			{
				return this._movieSynopsis;
			}
			set
			{
				if (((_movieSynopsis == value) 
							== false))
				{
					this.OnMovieSynopsisChanging(value);
					this.SendPropertyChanging();
					this._movieSynopsis = value;
					this.SendPropertyChanged("MovieSynopsis");
					this.OnMovieSynopsisChanged();
				}
			}
		}
		
		[Column(Storage="_movieYear", Name="MovieYear", DbType="varchar(4)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MovieYear
		{
			get
			{
				return this._movieYear;
			}
			set
			{
				if (((_movieYear == value) 
							== false))
				{
					this.OnMovieYearChanging(value);
					this.SendPropertyChanging();
					this._movieYear = value;
					this.SendPropertyChanged("MovieYear");
					this.OnMovieYearChanged();
				}
			}
		}
		
		[Column(Storage="_ratingID", Name="RatingID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long RatingID
		{
			get
			{
				return this._ratingID;
			}
			set
			{
				if ((_ratingID != value))
				{
					this.OnRatingIDChanging(value);
					this.SendPropertyChanging();
					this._ratingID = value;
					this.SendPropertyChanged("RatingID");
					this.OnRatingIDChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		[Column(Storage="_trailerUrlS", Name="TrailerURLs", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string TrailerUrlS
		{
			get
			{
				return this._trailerUrlS;
			}
			set
			{
				if (((_trailerUrlS == value) 
							== false))
				{
					this.OnTrailerUrlSChanging(value);
					this.SendPropertyChanging();
					this._trailerUrlS = value;
					this.SendPropertyChanged("TrailerUrlS");
					this.OnTrailerUrlSChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsMovieBuffet")]
	public partial class MsMovieBuffet : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private string _movieBuffetDesc;
		
		private long _movieBuffetID;
		
		private string _movieBuffetName;
		
		private string _movieBuffetType;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnMovieBuffetDescChanged();
		
		partial void OnMovieBuffetDescChanging(string value);
		
		partial void OnMovieBuffetIDChanged();
		
		partial void OnMovieBuffetIDChanging(long value);
		
		partial void OnMovieBuffetNameChanged();
		
		partial void OnMovieBuffetNameChanging(string value);
		
		partial void OnMovieBuffetTypeChanged();
		
		partial void OnMovieBuffetTypeChanging(string value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsMovieBuffet()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_movieBuffetDesc", Name="MovieBuffetDesc", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MovieBuffetDesc
		{
			get
			{
				return this._movieBuffetDesc;
			}
			set
			{
				if (((_movieBuffetDesc == value) 
							== false))
				{
					this.OnMovieBuffetDescChanging(value);
					this.SendPropertyChanging();
					this._movieBuffetDesc = value;
					this.SendPropertyChanged("MovieBuffetDesc");
					this.OnMovieBuffetDescChanged();
				}
			}
		}
		
		[Column(Storage="_movieBuffetID", Name="MovieBuffetID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long MovieBuffetID
		{
			get
			{
				return this._movieBuffetID;
			}
			set
			{
				if ((_movieBuffetID != value))
				{
					this.OnMovieBuffetIDChanging(value);
					this.SendPropertyChanging();
					this._movieBuffetID = value;
					this.SendPropertyChanged("MovieBuffetID");
					this.OnMovieBuffetIDChanged();
				}
			}
		}
		
		[Column(Storage="_movieBuffetName", Name="MovieBuffetName", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MovieBuffetName
		{
			get
			{
				return this._movieBuffetName;
			}
			set
			{
				if (((_movieBuffetName == value) 
							== false))
				{
					this.OnMovieBuffetNameChanging(value);
					this.SendPropertyChanging();
					this._movieBuffetName = value;
					this.SendPropertyChanged("MovieBuffetName");
					this.OnMovieBuffetNameChanged();
				}
			}
		}
		
		[Column(Storage="_movieBuffetType", Name="MovieBuffetType", DbType="char(1)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MovieBuffetType
		{
			get
			{
				return this._movieBuffetType;
			}
			set
			{
				if (((_movieBuffetType == value) 
							== false))
				{
					this.OnMovieBuffetTypeChanging(value);
					this.SendPropertyChanging();
					this._movieBuffetType = value;
					this.SendPropertyChanged("MovieBuffetType");
					this.OnMovieBuffetTypeChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsMovieBuffetDt")]
	public partial class MsMovieBuffetDT : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private long _movieBuffetDtID;
		
		private long _movieBuffetID;
		
		private long _movieID;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnMovieBuffetDtIDChanged();
		
		partial void OnMovieBuffetDtIDChanging(long value);
		
		partial void OnMovieBuffetIDChanged();
		
		partial void OnMovieBuffetIDChanging(long value);
		
		partial void OnMovieIDChanged();
		
		partial void OnMovieIDChanging(long value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsMovieBuffetDT()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_movieBuffetDtID", Name="MovieBuffetDtID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long MovieBuffetDtID
		{
			get
			{
				return this._movieBuffetDtID;
			}
			set
			{
				if ((_movieBuffetDtID != value))
				{
					this.OnMovieBuffetDtIDChanging(value);
					this.SendPropertyChanging();
					this._movieBuffetDtID = value;
					this.SendPropertyChanged("MovieBuffetDtID");
					this.OnMovieBuffetDtIDChanged();
				}
			}
		}
		
		[Column(Storage="_movieBuffetID", Name="MovieBuffetID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long MovieBuffetID
		{
			get
			{
				return this._movieBuffetID;
			}
			set
			{
				if ((_movieBuffetID != value))
				{
					this.OnMovieBuffetIDChanging(value);
					this.SendPropertyChanging();
					this._movieBuffetID = value;
					this.SendPropertyChanged("MovieBuffetID");
					this.OnMovieBuffetIDChanged();
				}
			}
		}
		
		[Column(Storage="_movieID", Name="MovieID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long MovieID
		{
			get
			{
				return this._movieID;
			}
			set
			{
				if ((_movieID != value))
				{
					this.OnMovieIDChanging(value);
					this.SendPropertyChanging();
					this._movieID = value;
					this.SendPropertyChanged("MovieID");
					this.OnMovieIDChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsMovieCategory")]
	public partial class MsMovieCategory : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _categoryDesc;
		
		private long _categoryID;
		
		private string _categoryName;
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _image;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private System.Nullable<int> _rowStatus;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCategoryDescChanged();
		
		partial void OnCategoryDescChanging(string value);
		
		partial void OnCategoryIDChanged();
		
		partial void OnCategoryIDChanging(long value);
		
		partial void OnCategoryNameChanged();
		
		partial void OnCategoryNameChanging(string value);
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImageChanged();
		
		partial void OnImageChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(System.Nullable<int> value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsMovieCategory()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_categoryDesc", Name="CategoryDesc", DbType="varchar(500)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CategoryDesc
		{
			get
			{
				return this._categoryDesc;
			}
			set
			{
				if (((_categoryDesc == value) 
							== false))
				{
					this.OnCategoryDescChanging(value);
					this.SendPropertyChanging();
					this._categoryDesc = value;
					this.SendPropertyChanged("CategoryDesc");
					this.OnCategoryDescChanged();
				}
			}
		}
		
		[Column(Storage="_categoryID", Name="CategoryID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long CategoryID
		{
			get
			{
				return this._categoryID;
			}
			set
			{
				if ((_categoryID != value))
				{
					this.OnCategoryIDChanging(value);
					this.SendPropertyChanging();
					this._categoryID = value;
					this.SendPropertyChanged("CategoryID");
					this.OnCategoryIDChanged();
				}
			}
		}
		
		[Column(Storage="_categoryName", Name="CategoryName", DbType="varchar(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string CategoryName
		{
			get
			{
				return this._categoryName;
			}
			set
			{
				if (((_categoryName == value) 
							== false))
				{
					this.OnCategoryNameChanging(value);
					this.SendPropertyChanging();
					this._categoryName = value;
					this.SendPropertyChanged("CategoryName");
					this.OnCategoryNameChanged();
				}
			}
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_image", Name="Image", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Image
		{
			get
			{
				return this._image;
			}
			set
			{
				if (((_image == value) 
							== false))
				{
					this.OnImageChanging(value);
					this.SendPropertyChanging();
					this._image = value;
					this.SendPropertyChanged("Image");
					this.OnImageChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsMovieLanguage")]
	public partial class MsMovieLanguage : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private long _languageID;
		
		private string _languageName;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private System.Nullable<int> _rowStatus;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnLanguageIDChanged();
		
		partial void OnLanguageIDChanging(long value);
		
		partial void OnLanguageNameChanged();
		
		partial void OnLanguageNameChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(System.Nullable<int> value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsMovieLanguage()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_languageID", Name="LanguageID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long LanguageID
		{
			get
			{
				return this._languageID;
			}
			set
			{
				if ((_languageID != value))
				{
					this.OnLanguageIDChanging(value);
					this.SendPropertyChanging();
					this._languageID = value;
					this.SendPropertyChanged("LanguageID");
					this.OnLanguageIDChanged();
				}
			}
		}
		
		[Column(Storage="_languageName", Name="LanguageName", DbType="varchar(50)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string LanguageName
		{
			get
			{
				return this._languageName;
			}
			set
			{
				if (((_languageName == value) 
							== false))
				{
					this.OnLanguageNameChanging(value);
					this.SendPropertyChanging();
					this._languageName = value;
					this.SendPropertyChanged("LanguageName");
					this.OnLanguageNameChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsMovieRating")]
	public partial class MsMovieRating : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private string _ratingCode;
		
		private string _ratingDesc;
		
		private long _ratingID;
		
		private string _ratingName;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRatingCodeChanged();
		
		partial void OnRatingCodeChanging(string value);
		
		partial void OnRatingDescChanged();
		
		partial void OnRatingDescChanging(string value);
		
		partial void OnRatingIDChanged();
		
		partial void OnRatingIDChanging(long value);
		
		partial void OnRatingNameChanged();
		
		partial void OnRatingNameChanging(string value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsMovieRating()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_ratingCode", Name="RatingCode", DbType="varchar(10)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string RatingCode
		{
			get
			{
				return this._ratingCode;
			}
			set
			{
				if (((_ratingCode == value) 
							== false))
				{
					this.OnRatingCodeChanging(value);
					this.SendPropertyChanging();
					this._ratingCode = value;
					this.SendPropertyChanged("RatingCode");
					this.OnRatingCodeChanged();
				}
			}
		}
		
		[Column(Storage="_ratingDesc", Name="RatingDesc", DbType="varchar(500)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string RatingDesc
		{
			get
			{
				return this._ratingDesc;
			}
			set
			{
				if (((_ratingDesc == value) 
							== false))
				{
					this.OnRatingDescChanging(value);
					this.SendPropertyChanging();
					this._ratingDesc = value;
					this.SendPropertyChanged("RatingDesc");
					this.OnRatingDescChanged();
				}
			}
		}
		
		[Column(Storage="_ratingID", Name="RatingID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long RatingID
		{
			get
			{
				return this._ratingID;
			}
			set
			{
				if ((_ratingID != value))
				{
					this.OnRatingIDChanging(value);
					this.SendPropertyChanging();
					this._ratingID = value;
					this.SendPropertyChanged("RatingID");
					this.OnRatingIDChanged();
				}
			}
		}
		
		[Column(Storage="_ratingName", Name="RatingName", DbType="varchar(50)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string RatingName
		{
			get
			{
				return this._ratingName;
			}
			set
			{
				if (((_ratingName == value) 
							== false))
				{
					this.OnRatingNameChanging(value);
					this.SendPropertyChanging();
					this._ratingName = value;
					this.SendPropertyChanged("RatingName");
					this.OnRatingNameChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsMusicAlbum")]
	public partial class MsMusicAlbum : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private long _albumID;
		
		private string _albumName;
		
		private long _artiseID;
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnAlbumIDChanged();
		
		partial void OnAlbumIDChanging(long value);
		
		partial void OnAlbumNameChanged();
		
		partial void OnAlbumNameChanging(string value);
		
		partial void OnArtiseIDChanged();
		
		partial void OnArtiseIDChanging(long value);
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsMusicAlbum()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_albumID", Name="AlbumID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long AlbumID
		{
			get
			{
				return this._albumID;
			}
			set
			{
				if ((_albumID != value))
				{
					this.OnAlbumIDChanging(value);
					this.SendPropertyChanging();
					this._albumID = value;
					this.SendPropertyChanged("AlbumID");
					this.OnAlbumIDChanged();
				}
			}
		}
		
		[Column(Storage="_albumName", Name="AlbumName", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string AlbumName
		{
			get
			{
				return this._albumName;
			}
			set
			{
				if (((_albumName == value) 
							== false))
				{
					this.OnAlbumNameChanging(value);
					this.SendPropertyChanging();
					this._albumName = value;
					this.SendPropertyChanged("AlbumName");
					this.OnAlbumNameChanged();
				}
			}
		}
		
		[Column(Storage="_artiseID", Name="ArtiseID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long ArtiseID
		{
			get
			{
				return this._artiseID;
			}
			set
			{
				if ((_artiseID != value))
				{
					this.OnArtiseIDChanging(value);
					this.SendPropertyChanging();
					this._artiseID = value;
					this.SendPropertyChanged("ArtiseID");
					this.OnArtiseIDChanged();
				}
			}
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsMusicArtise")]
	public partial class MsMusicArtIsE : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private long _artiseID;
		
		private string _artiseName;
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnArtiseIDChanged();
		
		partial void OnArtiseIDChanging(long value);
		
		partial void OnArtiseNameChanged();
		
		partial void OnArtiseNameChanging(string value);
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsMusicArtIsE()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_artiseID", Name="ArtiseID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long ArtiseID
		{
			get
			{
				return this._artiseID;
			}
			set
			{
				if ((_artiseID != value))
				{
					this.OnArtiseIDChanging(value);
					this.SendPropertyChanging();
					this._artiseID = value;
					this.SendPropertyChanged("ArtiseID");
					this.OnArtiseIDChanged();
				}
			}
		}
		
		[Column(Storage="_artiseName", Name="ArtiseName", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ArtiseName
		{
			get
			{
				return this._artiseName;
			}
			set
			{
				if (((_artiseName == value) 
							== false))
				{
					this.OnArtiseNameChanging(value);
					this.SendPropertyChanging();
					this._artiseName = value;
					this.SendPropertyChanged("ArtiseName");
					this.OnArtiseNameChanged();
				}
			}
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsMusicBuffet")]
	public partial class MsMusicBuffet : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private string _musicBuffetDesc;
		
		private long _musicBuffetID;
		
		private string _musicBuffetName;
		
		private string _musicBuffetType;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnMusicBuffetDescChanged();
		
		partial void OnMusicBuffetDescChanging(string value);
		
		partial void OnMusicBuffetIDChanged();
		
		partial void OnMusicBuffetIDChanging(long value);
		
		partial void OnMusicBuffetNameChanged();
		
		partial void OnMusicBuffetNameChanging(string value);
		
		partial void OnMusicBuffetTypeChanged();
		
		partial void OnMusicBuffetTypeChanging(string value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsMusicBuffet()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_musicBuffetDesc", Name="MusicBuffetDesc", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MusicBuffetDesc
		{
			get
			{
				return this._musicBuffetDesc;
			}
			set
			{
				if (((_musicBuffetDesc == value) 
							== false))
				{
					this.OnMusicBuffetDescChanging(value);
					this.SendPropertyChanging();
					this._musicBuffetDesc = value;
					this.SendPropertyChanged("MusicBuffetDesc");
					this.OnMusicBuffetDescChanged();
				}
			}
		}
		
		[Column(Storage="_musicBuffetID", Name="MusicBuffetID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long MusicBuffetID
		{
			get
			{
				return this._musicBuffetID;
			}
			set
			{
				if ((_musicBuffetID != value))
				{
					this.OnMusicBuffetIDChanging(value);
					this.SendPropertyChanging();
					this._musicBuffetID = value;
					this.SendPropertyChanged("MusicBuffetID");
					this.OnMusicBuffetIDChanged();
				}
			}
		}
		
		[Column(Storage="_musicBuffetName", Name="MusicBuffetName", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MusicBuffetName
		{
			get
			{
				return this._musicBuffetName;
			}
			set
			{
				if (((_musicBuffetName == value) 
							== false))
				{
					this.OnMusicBuffetNameChanging(value);
					this.SendPropertyChanging();
					this._musicBuffetName = value;
					this.SendPropertyChanged("MusicBuffetName");
					this.OnMusicBuffetNameChanged();
				}
			}
		}
		
		[Column(Storage="_musicBuffetType", Name="MusicBuffetType", DbType="char(1)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MusicBuffetType
		{
			get
			{
				return this._musicBuffetType;
			}
			set
			{
				if (((_musicBuffetType == value) 
							== false))
				{
					this.OnMusicBuffetTypeChanging(value);
					this.SendPropertyChanging();
					this._musicBuffetType = value;
					this.SendPropertyChanged("MusicBuffetType");
					this.OnMusicBuffetTypeChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsMusicBuffetDt")]
	public partial class MsMusicBuffetDT : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private long _albumID;
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private long _musicBuffetDtID;
		
		private long _musicBuffetID;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnAlbumIDChanged();
		
		partial void OnAlbumIDChanging(long value);
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnMusicBuffetDtIDChanged();
		
		partial void OnMusicBuffetDtIDChanging(long value);
		
		partial void OnMusicBuffetIDChanged();
		
		partial void OnMusicBuffetIDChanging(long value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsMusicBuffetDT()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_albumID", Name="AlbumID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long AlbumID
		{
			get
			{
				return this._albumID;
			}
			set
			{
				if ((_albumID != value))
				{
					this.OnAlbumIDChanging(value);
					this.SendPropertyChanging();
					this._albumID = value;
					this.SendPropertyChanged("AlbumID");
					this.OnAlbumIDChanged();
				}
			}
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_musicBuffetDtID", Name="MusicBuffetDtID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long MusicBuffetDtID
		{
			get
			{
				return this._musicBuffetDtID;
			}
			set
			{
				if ((_musicBuffetDtID != value))
				{
					this.OnMusicBuffetDtIDChanging(value);
					this.SendPropertyChanging();
					this._musicBuffetDtID = value;
					this.SendPropertyChanged("MusicBuffetDtID");
					this.OnMusicBuffetDtIDChanged();
				}
			}
		}
		
		[Column(Storage="_musicBuffetID", Name="MusicBuffetID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long MusicBuffetID
		{
			get
			{
				return this._musicBuffetID;
			}
			set
			{
				if ((_musicBuffetID != value))
				{
					this.OnMusicBuffetIDChanging(value);
					this.SendPropertyChanging();
					this._musicBuffetID = value;
					this.SendPropertyChanged("MusicBuffetID");
					this.OnMusicBuffetIDChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsMusicSong")]
	public partial class MsMusicSong : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private long _albumID;
		
		private long _artisID;
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private int _rowStatus;
		
		private long _songID;
		
		private string _songName;
		
		private string _songPath;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnAlbumIDChanged();
		
		partial void OnAlbumIDChanging(long value);
		
		partial void OnArtisIDChanged();
		
		partial void OnArtisIDChanging(long value);
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnSongIDChanged();
		
		partial void OnSongIDChanging(long value);
		
		partial void OnSongNameChanged();
		
		partial void OnSongNameChanging(string value);
		
		partial void OnSongPathChanged();
		
		partial void OnSongPathChanging(string value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsMusicSong()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_albumID", Name="AlbumID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long AlbumID
		{
			get
			{
				return this._albumID;
			}
			set
			{
				if ((_albumID != value))
				{
					this.OnAlbumIDChanging(value);
					this.SendPropertyChanging();
					this._albumID = value;
					this.SendPropertyChanged("AlbumID");
					this.OnAlbumIDChanged();
				}
			}
		}
		
		[Column(Storage="_artisID", Name="ArtisID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long ArtisID
		{
			get
			{
				return this._artisID;
			}
			set
			{
				if ((_artisID != value))
				{
					this.OnArtisIDChanging(value);
					this.SendPropertyChanging();
					this._artisID = value;
					this.SendPropertyChanged("ArtisID");
					this.OnArtisIDChanged();
				}
			}
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_songID", Name="SongID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long SongID
		{
			get
			{
				return this._songID;
			}
			set
			{
				if ((_songID != value))
				{
					this.OnSongIDChanging(value);
					this.SendPropertyChanging();
					this._songID = value;
					this.SendPropertyChanged("SongID");
					this.OnSongIDChanged();
				}
			}
		}
		
		[Column(Storage="_songName", Name="SongName", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string SongName
		{
			get
			{
				return this._songName;
			}
			set
			{
				if (((_songName == value) 
							== false))
				{
					this.OnSongNameChanging(value);
					this.SendPropertyChanging();
					this._songName = value;
					this.SendPropertyChanged("SongName");
					this.OnSongNameChanged();
				}
			}
		}
		
		[Column(Storage="_songPath", Name="SongPath", DbType="varchar(200)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string SongPath
		{
			get
			{
				return this._songPath;
			}
			set
			{
				if (((_songPath == value) 
							== false))
				{
					this.OnSongPathChanging(value);
					this.SendPropertyChanging();
					this._songPath = value;
					this.SendPropertyChanged("SongPath");
					this.OnSongPathChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsNews")]
	public partial class MsNews : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private long _newID;
		
		private long _newsCategoryID;
		
		private string _newsDesc;
		
		private string _newsHeadline;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnNewIDChanged();
		
		partial void OnNewIDChanging(long value);
		
		partial void OnNewsCategoryIDChanged();
		
		partial void OnNewsCategoryIDChanging(long value);
		
		partial void OnNewsDescChanged();
		
		partial void OnNewsDescChanging(string value);
		
		partial void OnNewsHeadlineChanged();
		
		partial void OnNewsHeadlineChanging(string value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsNews()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_newID", Name="NewID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long NewID
		{
			get
			{
				return this._newID;
			}
			set
			{
				if ((_newID != value))
				{
					this.OnNewIDChanging(value);
					this.SendPropertyChanging();
					this._newID = value;
					this.SendPropertyChanged("NewID");
					this.OnNewIDChanged();
				}
			}
		}
		
		[Column(Storage="_newsCategoryID", Name="NewsCategoryID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long NewsCategoryID
		{
			get
			{
				return this._newsCategoryID;
			}
			set
			{
				if ((_newsCategoryID != value))
				{
					this.OnNewsCategoryIDChanging(value);
					this.SendPropertyChanging();
					this._newsCategoryID = value;
					this.SendPropertyChanged("NewsCategoryID");
					this.OnNewsCategoryIDChanged();
				}
			}
		}
		
		[Column(Storage="_newsDesc", Name="NewsDesc", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string NewsDesc
		{
			get
			{
				return this._newsDesc;
			}
			set
			{
				if (((_newsDesc == value) 
							== false))
				{
					this.OnNewsDescChanging(value);
					this.SendPropertyChanging();
					this._newsDesc = value;
					this.SendPropertyChanged("NewsDesc");
					this.OnNewsDescChanged();
				}
			}
		}
		
		[Column(Storage="_newsHeadline", Name="NewsHeadline", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string NewsHeadline
		{
			get
			{
				return this._newsHeadline;
			}
			set
			{
				if (((_newsHeadline == value) 
							== false))
				{
					this.OnNewsHeadlineChanging(value);
					this.SendPropertyChanging();
					this._newsHeadline = value;
					this.SendPropertyChanged("NewsHeadline");
					this.OnNewsHeadlineChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsNewsCategory")]
	public partial class MsNewsCategory : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private string _newsCategoryDesc;
		
		private long _newsCategoryID;
		
		private string _newsCategoryName;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnNewsCategoryDescChanged();
		
		partial void OnNewsCategoryDescChanging(string value);
		
		partial void OnNewsCategoryIDChanged();
		
		partial void OnNewsCategoryIDChanging(long value);
		
		partial void OnNewsCategoryNameChanged();
		
		partial void OnNewsCategoryNameChanging(string value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsNewsCategory()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_newsCategoryDesc", Name="NewsCategoryDesc", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string NewsCategoryDesc
		{
			get
			{
				return this._newsCategoryDesc;
			}
			set
			{
				if (((_newsCategoryDesc == value) 
							== false))
				{
					this.OnNewsCategoryDescChanging(value);
					this.SendPropertyChanging();
					this._newsCategoryDesc = value;
					this.SendPropertyChanged("NewsCategoryDesc");
					this.OnNewsCategoryDescChanged();
				}
			}
		}
		
		[Column(Storage="_newsCategoryID", Name="NewsCategoryID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long NewsCategoryID
		{
			get
			{
				return this._newsCategoryID;
			}
			set
			{
				if ((_newsCategoryID != value))
				{
					this.OnNewsCategoryIDChanging(value);
					this.SendPropertyChanging();
					this._newsCategoryID = value;
					this.SendPropertyChanged("NewsCategoryID");
					this.OnNewsCategoryIDChanged();
				}
			}
		}
		
		[Column(Storage="_newsCategoryName", Name="NewsCategoryName", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string NewsCategoryName
		{
			get
			{
				return this._newsCategoryName;
			}
			set
			{
				if (((_newsCategoryName == value) 
							== false))
				{
					this.OnNewsCategoryNameChanging(value);
					this.SendPropertyChanging();
					this._newsCategoryName = value;
					this.SendPropertyChanged("NewsCategoryName");
					this.OnNewsCategoryNameChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsRadioCategory")]
	public partial class MsRadioCategory : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private System.Nullable<System.DateTime> _radioCategoryDesc;
		
		private long _radioCategoryID;
		
		private string _radioCategoryName;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRadioCategoryDescChanged();
		
		partial void OnRadioCategoryDescChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRadioCategoryIDChanged();
		
		partial void OnRadioCategoryIDChanging(long value);
		
		partial void OnRadioCategoryNameChanged();
		
		partial void OnRadioCategoryNameChanging(string value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsRadioCategory()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_radioCategoryDesc", Name="RadioCategoryDesc", DbType="time", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> RadioCategoryDesc
		{
			get
			{
				return this._radioCategoryDesc;
			}
			set
			{
				if ((_radioCategoryDesc != value))
				{
					this.OnRadioCategoryDescChanging(value);
					this.SendPropertyChanging();
					this._radioCategoryDesc = value;
					this.SendPropertyChanged("RadioCategoryDesc");
					this.OnRadioCategoryDescChanged();
				}
			}
		}
		
		[Column(Storage="_radioCategoryID", Name="RadioCategoryID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long RadioCategoryID
		{
			get
			{
				return this._radioCategoryID;
			}
			set
			{
				if ((_radioCategoryID != value))
				{
					this.OnRadioCategoryIDChanging(value);
					this.SendPropertyChanging();
					this._radioCategoryID = value;
					this.SendPropertyChanged("RadioCategoryID");
					this.OnRadioCategoryIDChanged();
				}
			}
		}
		
		[Column(Storage="_radioCategoryName", Name="RadioCategoryName", DbType="varchar(200)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string RadioCategoryName
		{
			get
			{
				return this._radioCategoryName;
			}
			set
			{
				if (((_radioCategoryName == value) 
							== false))
				{
					this.OnRadioCategoryNameChanging(value);
					this.SendPropertyChanging();
					this._radioCategoryName = value;
					this.SendPropertyChanged("RadioCategoryName");
					this.OnRadioCategoryNameChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsRadioStation")]
	public partial class MsRadioStation : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private long _radioCategoryID;
		
		private string _radioStasionDesc;
		
		private long _radioStationID;
		
		private string _radioStationName;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRadioCategoryIDChanged();
		
		partial void OnRadioCategoryIDChanging(long value);
		
		partial void OnRadioStasionDescChanged();
		
		partial void OnRadioStasionDescChanging(string value);
		
		partial void OnRadioStationIDChanged();
		
		partial void OnRadioStationIDChanging(long value);
		
		partial void OnRadioStationNameChanged();
		
		partial void OnRadioStationNameChanging(string value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsRadioStation()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_radioCategoryID", Name="RadioCategoryID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long RadioCategoryID
		{
			get
			{
				return this._radioCategoryID;
			}
			set
			{
				if ((_radioCategoryID != value))
				{
					this.OnRadioCategoryIDChanging(value);
					this.SendPropertyChanging();
					this._radioCategoryID = value;
					this.SendPropertyChanged("RadioCategoryID");
					this.OnRadioCategoryIDChanged();
				}
			}
		}
		
		[Column(Storage="_radioStasionDesc", Name="RadioStasionDesc", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string RadioStasionDesc
		{
			get
			{
				return this._radioStasionDesc;
			}
			set
			{
				if (((_radioStasionDesc == value) 
							== false))
				{
					this.OnRadioStasionDescChanging(value);
					this.SendPropertyChanging();
					this._radioStasionDesc = value;
					this.SendPropertyChanged("RadioStasionDesc");
					this.OnRadioStasionDescChanged();
				}
			}
		}
		
		[Column(Storage="_radioStationID", Name="RadioStationID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long RadioStationID
		{
			get
			{
				return this._radioStationID;
			}
			set
			{
				if ((_radioStationID != value))
				{
					this.OnRadioStationIDChanging(value);
					this.SendPropertyChanging();
					this._radioStationID = value;
					this.SendPropertyChanged("RadioStationID");
					this.OnRadioStationIDChanged();
				}
			}
		}
		
		[Column(Storage="_radioStationName", Name="RadioStationName", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string RadioStationName
		{
			get
			{
				return this._radioStationName;
			}
			set
			{
				if (((_radioStationName == value) 
							== false))
				{
					this.OnRadioStationNameChanging(value);
					this.SendPropertyChanging();
					this._radioStationName = value;
					this.SendPropertyChanged("RadioStationName");
					this.OnRadioStationNameChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsRoomServiceCategory")]
	public partial class MsRoomServiceCategory : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private string _roomServiceCategoryDesc;
		
		private long _roomServiceCategoryID;
		
		private string _roomServiceCategoryName;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRoomServiceCategoryDescChanged();
		
		partial void OnRoomServiceCategoryDescChanging(string value);
		
		partial void OnRoomServiceCategoryIDChanged();
		
		partial void OnRoomServiceCategoryIDChanging(long value);
		
		partial void OnRoomServiceCategoryNameChanged();
		
		partial void OnRoomServiceCategoryNameChanging(string value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsRoomServiceCategory()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_roomServiceCategoryDesc", Name="RoomServiceCategoryDesc", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string RoomServiceCategoryDesc
		{
			get
			{
				return this._roomServiceCategoryDesc;
			}
			set
			{
				if (((_roomServiceCategoryDesc == value) 
							== false))
				{
					this.OnRoomServiceCategoryDescChanging(value);
					this.SendPropertyChanging();
					this._roomServiceCategoryDesc = value;
					this.SendPropertyChanged("RoomServiceCategoryDesc");
					this.OnRoomServiceCategoryDescChanged();
				}
			}
		}
		
		[Column(Storage="_roomServiceCategoryID", Name="RoomServiceCategoryID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long RoomServiceCategoryID
		{
			get
			{
				return this._roomServiceCategoryID;
			}
			set
			{
				if ((_roomServiceCategoryID != value))
				{
					this.OnRoomServiceCategoryIDChanging(value);
					this.SendPropertyChanging();
					this._roomServiceCategoryID = value;
					this.SendPropertyChanged("RoomServiceCategoryID");
					this.OnRoomServiceCategoryIDChanged();
				}
			}
		}
		
		[Column(Storage="_roomServiceCategoryName", Name="RoomServiceCategoryName", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string RoomServiceCategoryName
		{
			get
			{
				return this._roomServiceCategoryName;
			}
			set
			{
				if (((_roomServiceCategoryName == value) 
							== false))
				{
					this.OnRoomServiceCategoryNameChanging(value);
					this.SendPropertyChanging();
					this._roomServiceCategoryName = value;
					this.SendPropertyChanged("RoomServiceCategoryName");
					this.OnRoomServiceCategoryNameChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsRoomServiceItem")]
	public partial class MsRoomServiceItem : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private long _roomServiceCategoryID;
		
		private string _roomServiceItemDesc;
		
		private long _roomServiceItemID;
		
		private string _roomServiceItemName;
		
		private System.Nullable<int> _roomServiceItemPrice;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRoomServiceCategoryIDChanged();
		
		partial void OnRoomServiceCategoryIDChanging(long value);
		
		partial void OnRoomServiceItemDescChanged();
		
		partial void OnRoomServiceItemDescChanging(string value);
		
		partial void OnRoomServiceItemIDChanged();
		
		partial void OnRoomServiceItemIDChanging(long value);
		
		partial void OnRoomServiceItemNameChanged();
		
		partial void OnRoomServiceItemNameChanging(string value);
		
		partial void OnRoomServiceItemPriceChanged();
		
		partial void OnRoomServiceItemPriceChanging(System.Nullable<int> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsRoomServiceItem()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_roomServiceCategoryID", Name="RoomServiceCategoryID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long RoomServiceCategoryID
		{
			get
			{
				return this._roomServiceCategoryID;
			}
			set
			{
				if ((_roomServiceCategoryID != value))
				{
					this.OnRoomServiceCategoryIDChanging(value);
					this.SendPropertyChanging();
					this._roomServiceCategoryID = value;
					this.SendPropertyChanged("RoomServiceCategoryID");
					this.OnRoomServiceCategoryIDChanged();
				}
			}
		}
		
		[Column(Storage="_roomServiceItemDesc", Name="RoomServiceItemDesc", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string RoomServiceItemDesc
		{
			get
			{
				return this._roomServiceItemDesc;
			}
			set
			{
				if (((_roomServiceItemDesc == value) 
							== false))
				{
					this.OnRoomServiceItemDescChanging(value);
					this.SendPropertyChanging();
					this._roomServiceItemDesc = value;
					this.SendPropertyChanged("RoomServiceItemDesc");
					this.OnRoomServiceItemDescChanged();
				}
			}
		}
		
		[Column(Storage="_roomServiceItemID", Name="RoomServiceItemID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long RoomServiceItemID
		{
			get
			{
				return this._roomServiceItemID;
			}
			set
			{
				if ((_roomServiceItemID != value))
				{
					this.OnRoomServiceItemIDChanging(value);
					this.SendPropertyChanging();
					this._roomServiceItemID = value;
					this.SendPropertyChanged("RoomServiceItemID");
					this.OnRoomServiceItemIDChanged();
				}
			}
		}
		
		[Column(Storage="_roomServiceItemName", Name="RoomServiceItemName", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string RoomServiceItemName
		{
			get
			{
				return this._roomServiceItemName;
			}
			set
			{
				if (((_roomServiceItemName == value) 
							== false))
				{
					this.OnRoomServiceItemNameChanging(value);
					this.SendPropertyChanging();
					this._roomServiceItemName = value;
					this.SendPropertyChanged("RoomServiceItemName");
					this.OnRoomServiceItemNameChanged();
				}
			}
		}
		
		[Column(Storage="_roomServiceItemPrice", Name="RoomServiceItemPrice", DbType="int", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> RoomServiceItemPrice
		{
			get
			{
				return this._roomServiceItemPrice;
			}
			set
			{
				if ((_roomServiceItemPrice != value))
				{
					this.OnRoomServiceItemPriceChanging(value);
					this.SendPropertyChanging();
					this._roomServiceItemPrice = value;
					this.SendPropertyChanged("RoomServiceItemPrice");
					this.OnRoomServiceItemPriceChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsShoppingCategory")]
	public partial class MsShoppingCategory : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _imagefile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private int _rowStatus;
		
		private string _shoppingCategoryDesc;
		
		private long _shoppingCategoryID;
		
		private string _shoppingCategoryName;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImagefileChanged();
		
		partial void OnImagefileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnShoppingCategoryDescChanged();
		
		partial void OnShoppingCategoryDescChanging(string value);
		
		partial void OnShoppingCategoryIDChanged();
		
		partial void OnShoppingCategoryIDChanging(long value);
		
		partial void OnShoppingCategoryNameChanged();
		
		partial void OnShoppingCategoryNameChanging(string value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsShoppingCategory()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_imagefile", Name="Imagefile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Imagefile
		{
			get
			{
				return this._imagefile;
			}
			set
			{
				if (((_imagefile == value) 
							== false))
				{
					this.OnImagefileChanging(value);
					this.SendPropertyChanging();
					this._imagefile = value;
					this.SendPropertyChanged("Imagefile");
					this.OnImagefileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_shoppingCategoryDesc", Name="ShoppingCategoryDesc", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ShoppingCategoryDesc
		{
			get
			{
				return this._shoppingCategoryDesc;
			}
			set
			{
				if (((_shoppingCategoryDesc == value) 
							== false))
				{
					this.OnShoppingCategoryDescChanging(value);
					this.SendPropertyChanging();
					this._shoppingCategoryDesc = value;
					this.SendPropertyChanged("ShoppingCategoryDesc");
					this.OnShoppingCategoryDescChanged();
				}
			}
		}
		
		[Column(Storage="_shoppingCategoryID", Name="ShoppingCategoryID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long ShoppingCategoryID
		{
			get
			{
				return this._shoppingCategoryID;
			}
			set
			{
				if ((_shoppingCategoryID != value))
				{
					this.OnShoppingCategoryIDChanging(value);
					this.SendPropertyChanging();
					this._shoppingCategoryID = value;
					this.SendPropertyChanged("ShoppingCategoryID");
					this.OnShoppingCategoryIDChanged();
				}
			}
		}
		
		[Column(Storage="_shoppingCategoryName", Name="ShoppingCategoryName", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ShoppingCategoryName
		{
			get
			{
				return this._shoppingCategoryName;
			}
			set
			{
				if (((_shoppingCategoryName == value) 
							== false))
				{
					this.OnShoppingCategoryNameChanging(value);
					this.SendPropertyChanging();
					this._shoppingCategoryName = value;
					this.SendPropertyChanged("ShoppingCategoryName");
					this.OnShoppingCategoryNameChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsShoppingItem")]
	public partial class MsShoppingItem : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private int _rowStatus;
		
		private System.Nullable<int> _shopingItemPrice;
		
		private System.Nullable<long> _shoppingCategoryID;
		
		private string _shoppingItemDesc;
		
		private long _shoppingItemID;
		
		private string _shoppingItemName;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnShopingItemPriceChanged();
		
		partial void OnShopingItemPriceChanging(System.Nullable<int> value);
		
		partial void OnShoppingCategoryIDChanged();
		
		partial void OnShoppingCategoryIDChanging(System.Nullable<long> value);
		
		partial void OnShoppingItemDescChanged();
		
		partial void OnShoppingItemDescChanging(string value);
		
		partial void OnShoppingItemIDChanged();
		
		partial void OnShoppingItemIDChanging(long value);
		
		partial void OnShoppingItemNameChanged();
		
		partial void OnShoppingItemNameChanging(string value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public MsShoppingItem()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_shopingItemPrice", Name="ShopingItemPrice", DbType="int", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> ShopingItemPrice
		{
			get
			{
				return this._shopingItemPrice;
			}
			set
			{
				if ((_shopingItemPrice != value))
				{
					this.OnShopingItemPriceChanging(value);
					this.SendPropertyChanging();
					this._shopingItemPrice = value;
					this.SendPropertyChanged("ShopingItemPrice");
					this.OnShopingItemPriceChanged();
				}
			}
		}
		
		[Column(Storage="_shoppingCategoryID", Name="ShoppingCategoryID", DbType="bigint(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<long> ShoppingCategoryID
		{
			get
			{
				return this._shoppingCategoryID;
			}
			set
			{
				if ((_shoppingCategoryID != value))
				{
					this.OnShoppingCategoryIDChanging(value);
					this.SendPropertyChanging();
					this._shoppingCategoryID = value;
					this.SendPropertyChanged("ShoppingCategoryID");
					this.OnShoppingCategoryIDChanged();
				}
			}
		}
		
		[Column(Storage="_shoppingItemDesc", Name="ShoppingItemDesc", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ShoppingItemDesc
		{
			get
			{
				return this._shoppingItemDesc;
			}
			set
			{
				if (((_shoppingItemDesc == value) 
							== false))
				{
					this.OnShoppingItemDescChanging(value);
					this.SendPropertyChanging();
					this._shoppingItemDesc = value;
					this.SendPropertyChanged("ShoppingItemDesc");
					this.OnShoppingItemDescChanged();
				}
			}
		}
		
		[Column(Storage="_shoppingItemID", Name="ShoppingItemID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long ShoppingItemID
		{
			get
			{
				return this._shoppingItemID;
			}
			set
			{
				if ((_shoppingItemID != value))
				{
					this.OnShoppingItemIDChanging(value);
					this.SendPropertyChanging();
					this._shoppingItemID = value;
					this.SendPropertyChanged("ShoppingItemID");
					this.OnShoppingItemIDChanged();
				}
			}
		}
		
		[Column(Storage="_shoppingItemName", Name="ShoppingItemName", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ShoppingItemName
		{
			get
			{
				return this._shoppingItemName;
			}
			set
			{
				if (((_shoppingItemName == value) 
							== false))
				{
					this.OnShoppingItemNameChanging(value);
					this.SendPropertyChanging();
					this._shoppingItemName = value;
					this.SendPropertyChanged("ShoppingItemName");
					this.OnShoppingItemNameChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsSTB")]
	public partial class MsSTB
	{
		
		private System.Nullable<sbyte> _fgAllowDowngrade;
		
		private System.Nullable<sbyte> _fgForceUpdate;
		
		private System.Nullable<sbyte> _fgLog;
		
		private System.Nullable<sbyte> _fgSsh;
		
		private System.Nullable<System.DateTime> _lastConnection;
		
		private string _macAddress;
		
		private string _providerID;
		
		private string _smartCard;
		
		private string _stbID;
		
		private string _stbrEmark;
		
		private System.Nullable<int> _uptime;
		
		private string _version;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnFgAllowDowngradeChanged();
		
		partial void OnFgAllowDowngradeChanging(System.Nullable<sbyte> value);
		
		partial void OnFgForceUpdateChanged();
		
		partial void OnFgForceUpdateChanging(System.Nullable<sbyte> value);
		
		partial void OnFgLogChanged();
		
		partial void OnFgLogChanging(System.Nullable<sbyte> value);
		
		partial void OnFgSshChanged();
		
		partial void OnFgSshChanging(System.Nullable<sbyte> value);
		
		partial void OnLastConnectionChanged();
		
		partial void OnLastConnectionChanging(System.Nullable<System.DateTime> value);
		
		partial void OnMacAddressChanged();
		
		partial void OnMacAddressChanging(string value);
		
		partial void OnProviderIDChanged();
		
		partial void OnProviderIDChanging(string value);
		
		partial void OnSmartCardChanged();
		
		partial void OnSmartCardChanging(string value);
		
		partial void OnSTBidChanged();
		
		partial void OnSTBidChanging(string value);
		
		partial void OnStbrEmarkChanged();
		
		partial void OnStbrEmarkChanging(string value);
		
		partial void OnUptimeChanged();
		
		partial void OnUptimeChanging(System.Nullable<int> value);
		
		partial void OnVersionChanged();
		
		partial void OnVersionChanging(string value);
		#endregion
		
		
		public MsSTB()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_fgAllowDowngrade", Name="fgAllowDowngrade", DbType="tinyint(1)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<sbyte> FgAllowDowngrade
		{
			get
			{
				return this._fgAllowDowngrade;
			}
			set
			{
				if ((_fgAllowDowngrade != value))
				{
					this.OnFgAllowDowngradeChanging(value);
					this._fgAllowDowngrade = value;
					this.OnFgAllowDowngradeChanged();
				}
			}
		}
		
		[Column(Storage="_fgForceUpdate", Name="fgForceUpdate", DbType="tinyint(1)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<sbyte> FgForceUpdate
		{
			get
			{
				return this._fgForceUpdate;
			}
			set
			{
				if ((_fgForceUpdate != value))
				{
					this.OnFgForceUpdateChanging(value);
					this._fgForceUpdate = value;
					this.OnFgForceUpdateChanged();
				}
			}
		}
		
		[Column(Storage="_fgLog", Name="fgLog", DbType="tinyint(1)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<sbyte> FgLog
		{
			get
			{
				return this._fgLog;
			}
			set
			{
				if ((_fgLog != value))
				{
					this.OnFgLogChanging(value);
					this._fgLog = value;
					this.OnFgLogChanged();
				}
			}
		}
		
		[Column(Storage="_fgSsh", Name="fgSSH", DbType="tinyint(1)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<sbyte> FgSsh
		{
			get
			{
				return this._fgSsh;
			}
			set
			{
				if ((_fgSsh != value))
				{
					this.OnFgSshChanging(value);
					this._fgSsh = value;
					this.OnFgSshChanged();
				}
			}
		}
		
		[Column(Storage="_lastConnection", Name="LastConnection", DbType="timestamp", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> LastConnection
		{
			get
			{
				return this._lastConnection;
			}
			set
			{
				if ((_lastConnection != value))
				{
					this.OnLastConnectionChanging(value);
					this._lastConnection = value;
					this.OnLastConnectionChanged();
				}
			}
		}
		
		[Column(Storage="_macAddress", Name="MacAddress", DbType="varchar(17)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MacAddress
		{
			get
			{
				return this._macAddress;
			}
			set
			{
				if (((_macAddress == value) 
							== false))
				{
					this.OnMacAddressChanging(value);
					this._macAddress = value;
					this.OnMacAddressChanged();
				}
			}
		}
		
		[Column(Storage="_providerID", Name="ProviderID", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ProviderID
		{
			get
			{
				return this._providerID;
			}
			set
			{
				if (((_providerID == value) 
							== false))
				{
					this.OnProviderIDChanging(value);
					this._providerID = value;
					this.OnProviderIDChanged();
				}
			}
		}
		
		[Column(Storage="_smartCard", Name="SmartCard", DbType="varchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string SmartCard
		{
			get
			{
				return this._smartCard;
			}
			set
			{
				if (((_smartCard == value) 
							== false))
				{
					this.OnSmartCardChanging(value);
					this._smartCard = value;
					this.OnSmartCardChanged();
				}
			}
		}
		
		[Column(Storage="_stbID", Name="STBID", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string STBid
		{
			get
			{
				return this._stbID;
			}
			set
			{
				if (((_stbID == value) 
							== false))
				{
					this.OnSTBidChanging(value);
					this._stbID = value;
					this.OnSTBidChanged();
				}
			}
		}
		
		[Column(Storage="_stbrEmark", Name="STBRemark", DbType="varchar(200)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string StbrEmark
		{
			get
			{
				return this._stbrEmark;
			}
			set
			{
				if (((_stbrEmark == value) 
							== false))
				{
					this.OnStbrEmarkChanging(value);
					this._stbrEmark = value;
					this.OnStbrEmarkChanged();
				}
			}
		}
		
		[Column(Storage="_uptime", Name="Uptime", DbType="int", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> Uptime
		{
			get
			{
				return this._uptime;
			}
			set
			{
				if ((_uptime != value))
				{
					this.OnUptimeChanging(value);
					this._uptime = value;
					this.OnUptimeChanged();
				}
			}
		}
		
		[Column(Storage="_version", Name="Version", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Version
		{
			get
			{
				return this._version;
			}
			set
			{
				if (((_version == value) 
							== false))
				{
					this.OnVersionChanging(value);
					this._version = value;
					this.OnVersionChanged();
				}
			}
		}
	}
	
	[Table(Name="ITVDB.MsSTBModuleVersion")]
	public partial class MsSTBmOdUlEVersion
	{
		
		private string _moduleID;
		
		private string _stbmAcAddress;
		
		private string _version;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnModuleIDChanged();
		
		partial void OnModuleIDChanging(string value);
		
		partial void OnStbmAcAddressChanged();
		
		partial void OnStbmAcAddressChanging(string value);
		
		partial void OnVersionChanged();
		
		partial void OnVersionChanging(string value);
		#endregion
		
		
		public MsSTBmOdUlEVersion()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_moduleID", Name="ModuleID", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModuleID
		{
			get
			{
				return this._moduleID;
			}
			set
			{
				if (((_moduleID == value) 
							== false))
				{
					this.OnModuleIDChanging(value);
					this._moduleID = value;
					this.OnModuleIDChanged();
				}
			}
		}
		
		[Column(Storage="_stbmAcAddress", Name="STBMacAddress", DbType="varchar(17)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string StbmAcAddress
		{
			get
			{
				return this._stbmAcAddress;
			}
			set
			{
				if (((_stbmAcAddress == value) 
							== false))
				{
					this.OnStbmAcAddressChanging(value);
					this._stbmAcAddress = value;
					this.OnStbmAcAddressChanged();
				}
			}
		}
		
		[Column(Storage="_version", Name="Version", DbType="varchar(10)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Version
		{
			get
			{
				return this._version;
			}
			set
			{
				if (((_version == value) 
							== false))
				{
					this.OnVersionChanging(value);
					this._version = value;
					this.OnVersionChanged();
				}
			}
		}
	}
	
	[Table(Name="ITVDB.MsTourCategory")]
	public partial class MsTourCategory : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		private string _tourCategoryDesc;
		
		private long _tourCategoryID;
		
		private string _tourCategoryName;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		
		partial void OnTourCategoryDescChanged();
		
		partial void OnTourCategoryDescChanging(string value);
		
		partial void OnTourCategoryIDChanged();
		
		partial void OnTourCategoryIDChanging(long value);
		
		partial void OnTourCategoryNameChanged();
		
		partial void OnTourCategoryNameChanging(string value);
		#endregion
		
		
		public MsTourCategory()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		[Column(Storage="_tourCategoryDesc", Name="TourCategoryDesc", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string TourCategoryDesc
		{
			get
			{
				return this._tourCategoryDesc;
			}
			set
			{
				if (((_tourCategoryDesc == value) 
							== false))
				{
					this.OnTourCategoryDescChanging(value);
					this.SendPropertyChanging();
					this._tourCategoryDesc = value;
					this.SendPropertyChanged("TourCategoryDesc");
					this.OnTourCategoryDescChanged();
				}
			}
		}
		
		[Column(Storage="_tourCategoryID", Name="TourCategoryID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long TourCategoryID
		{
			get
			{
				return this._tourCategoryID;
			}
			set
			{
				if ((_tourCategoryID != value))
				{
					this.OnTourCategoryIDChanging(value);
					this.SendPropertyChanging();
					this._tourCategoryID = value;
					this.SendPropertyChanged("TourCategoryID");
					this.OnTourCategoryIDChanged();
				}
			}
		}
		
		[Column(Storage="_tourCategoryName", Name="TourCategoryName", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string TourCategoryName
		{
			get
			{
				return this._tourCategoryName;
			}
			set
			{
				if (((_tourCategoryName == value) 
							== false))
				{
					this.OnTourCategoryNameChanging(value);
					this.SendPropertyChanging();
					this._tourCategoryName = value;
					this.SendPropertyChanged("TourCategoryName");
					this.OnTourCategoryNameChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsTourDetail")]
	public partial class MsTourDetail : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		private long _tourCategoryID;
		
		private string _tourDetailDesc;
		
		private long _tourDetailID;
		
		private string _tourDetailName;
		
		private System.Nullable<int> _tourDetailPriceAdult;
		
		private System.Nullable<int> _tourDetailPriceChild;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		
		partial void OnTourCategoryIDChanged();
		
		partial void OnTourCategoryIDChanging(long value);
		
		partial void OnTourDetailDescChanged();
		
		partial void OnTourDetailDescChanging(string value);
		
		partial void OnTourDetailIDChanged();
		
		partial void OnTourDetailIDChanging(long value);
		
		partial void OnTourDetailNameChanged();
		
		partial void OnTourDetailNameChanging(string value);
		
		partial void OnTourDetailPriceAdultChanged();
		
		partial void OnTourDetailPriceAdultChanging(System.Nullable<int> value);
		
		partial void OnTourDetailPriceChildChanged();
		
		partial void OnTourDetailPriceChildChanging(System.Nullable<int> value);
		#endregion
		
		
		public MsTourDetail()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		[Column(Storage="_tourCategoryID", Name="TourCategoryID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long TourCategoryID
		{
			get
			{
				return this._tourCategoryID;
			}
			set
			{
				if ((_tourCategoryID != value))
				{
					this.OnTourCategoryIDChanging(value);
					this.SendPropertyChanging();
					this._tourCategoryID = value;
					this.SendPropertyChanged("TourCategoryID");
					this.OnTourCategoryIDChanged();
				}
			}
		}
		
		[Column(Storage="_tourDetailDesc", Name="TourDetailDesc", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string TourDetailDesc
		{
			get
			{
				return this._tourDetailDesc;
			}
			set
			{
				if (((_tourDetailDesc == value) 
							== false))
				{
					this.OnTourDetailDescChanging(value);
					this.SendPropertyChanging();
					this._tourDetailDesc = value;
					this.SendPropertyChanged("TourDetailDesc");
					this.OnTourDetailDescChanged();
				}
			}
		}
		
		[Column(Storage="_tourDetailID", Name="TourDetailID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long TourDetailID
		{
			get
			{
				return this._tourDetailID;
			}
			set
			{
				if ((_tourDetailID != value))
				{
					this.OnTourDetailIDChanging(value);
					this.SendPropertyChanging();
					this._tourDetailID = value;
					this.SendPropertyChanged("TourDetailID");
					this.OnTourDetailIDChanged();
				}
			}
		}
		
		[Column(Storage="_tourDetailName", Name="TourDetailName", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string TourDetailName
		{
			get
			{
				return this._tourDetailName;
			}
			set
			{
				if (((_tourDetailName == value) 
							== false))
				{
					this.OnTourDetailNameChanging(value);
					this.SendPropertyChanging();
					this._tourDetailName = value;
					this.SendPropertyChanged("TourDetailName");
					this.OnTourDetailNameChanged();
				}
			}
		}
		
		[Column(Storage="_tourDetailPriceAdult", Name="TourDetailPriceAdult", DbType="int", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> TourDetailPriceAdult
		{
			get
			{
				return this._tourDetailPriceAdult;
			}
			set
			{
				if ((_tourDetailPriceAdult != value))
				{
					this.OnTourDetailPriceAdultChanging(value);
					this.SendPropertyChanging();
					this._tourDetailPriceAdult = value;
					this.SendPropertyChanged("TourDetailPriceAdult");
					this.OnTourDetailPriceAdultChanged();
				}
			}
		}
		
		[Column(Storage="_tourDetailPriceChild", Name="TourDetailPriceChild", DbType="int", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> TourDetailPriceChild
		{
			get
			{
				return this._tourDetailPriceChild;
			}
			set
			{
				if ((_tourDetailPriceChild != value))
				{
					this.OnTourDetailPriceChildChanging(value);
					this.SendPropertyChanging();
					this._tourDetailPriceChild = value;
					this.SendPropertyChanged("TourDetailPriceChild");
					this.OnTourDetailPriceChildChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsTVCategory")]
	public partial class MsTvCAteGory : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		private string _tvcAtegoryDesc;
		
		private long _tvcAtegoryID;
		
		private string _tvcAtegoryName;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		
		partial void OnTvcAtegoryDescChanged();
		
		partial void OnTvcAtegoryDescChanging(string value);
		
		partial void OnTvcAtegoryIDChanged();
		
		partial void OnTvcAtegoryIDChanging(long value);
		
		partial void OnTvcAtegoryNameChanged();
		
		partial void OnTvcAtegoryNameChanging(string value);
		#endregion
		
		
		public MsTvCAteGory()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		[Column(Storage="_tvcAtegoryDesc", Name="TVCategoryDesc", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string TvcAtegoryDesc
		{
			get
			{
				return this._tvcAtegoryDesc;
			}
			set
			{
				if (((_tvcAtegoryDesc == value) 
							== false))
				{
					this.OnTvcAtegoryDescChanging(value);
					this.SendPropertyChanging();
					this._tvcAtegoryDesc = value;
					this.SendPropertyChanged("TvcAtegoryDesc");
					this.OnTvcAtegoryDescChanged();
				}
			}
		}
		
		[Column(Storage="_tvcAtegoryID", Name="TVCategoryID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long TvcAtegoryID
		{
			get
			{
				return this._tvcAtegoryID;
			}
			set
			{
				if ((_tvcAtegoryID != value))
				{
					this.OnTvcAtegoryIDChanging(value);
					this.SendPropertyChanging();
					this._tvcAtegoryID = value;
					this.SendPropertyChanged("TvcAtegoryID");
					this.OnTvcAtegoryIDChanged();
				}
			}
		}
		
		[Column(Storage="_tvcAtegoryName", Name="TVCategoryName", DbType="varchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string TvcAtegoryName
		{
			get
			{
				return this._tvcAtegoryName;
			}
			set
			{
				if (((_tvcAtegoryName == value) 
							== false))
				{
					this.OnTvcAtegoryNameChanging(value);
					this.SendPropertyChanging();
					this._tvcAtegoryName = value;
					this.SendPropertyChanged("TvcAtegoryName");
					this.OnTvcAtegoryNameChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsTVChannel")]
	public partial class MsTvCHanNeL : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		private long _tvcAtegoryID;
		
		private long _tvcHannelID;
		
		private string _tvcHannelName;
		
		private System.Nullable<int> _tvcHannelNo;
		
		private string _tvcHannelUrl;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		
		partial void OnTvcAtegoryIDChanged();
		
		partial void OnTvcAtegoryIDChanging(long value);
		
		partial void OnTvcHannelIDChanged();
		
		partial void OnTvcHannelIDChanging(long value);
		
		partial void OnTvcHannelNameChanged();
		
		partial void OnTvcHannelNameChanging(string value);
		
		partial void OnTvcHannelNoChanged();
		
		partial void OnTvcHannelNoChanging(System.Nullable<int> value);
		
		partial void OnTvcHannelUrlChanged();
		
		partial void OnTvcHannelUrlChanging(string value);
		#endregion
		
		
		public MsTvCHanNeL()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		[Column(Storage="_tvcAtegoryID", Name="TVCategoryID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long TvcAtegoryID
		{
			get
			{
				return this._tvcAtegoryID;
			}
			set
			{
				if ((_tvcAtegoryID != value))
				{
					this.OnTvcAtegoryIDChanging(value);
					this.SendPropertyChanging();
					this._tvcAtegoryID = value;
					this.SendPropertyChanged("TvcAtegoryID");
					this.OnTvcAtegoryIDChanged();
				}
			}
		}
		
		[Column(Storage="_tvcHannelID", Name="TVChannelID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long TvcHannelID
		{
			get
			{
				return this._tvcHannelID;
			}
			set
			{
				if ((_tvcHannelID != value))
				{
					this.OnTvcHannelIDChanging(value);
					this.SendPropertyChanging();
					this._tvcHannelID = value;
					this.SendPropertyChanged("TvcHannelID");
					this.OnTvcHannelIDChanged();
				}
			}
		}
		
		[Column(Storage="_tvcHannelName", Name="TVChannelName", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string TvcHannelName
		{
			get
			{
				return this._tvcHannelName;
			}
			set
			{
				if (((_tvcHannelName == value) 
							== false))
				{
					this.OnTvcHannelNameChanging(value);
					this.SendPropertyChanging();
					this._tvcHannelName = value;
					this.SendPropertyChanged("TvcHannelName");
					this.OnTvcHannelNameChanged();
				}
			}
		}
		
		[Column(Storage="_tvcHannelNo", Name="TVChannelNo", DbType="int", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> TvcHannelNo
		{
			get
			{
				return this._tvcHannelNo;
			}
			set
			{
				if ((_tvcHannelNo != value))
				{
					this.OnTvcHannelNoChanging(value);
					this.SendPropertyChanging();
					this._tvcHannelNo = value;
					this.SendPropertyChanged("TvcHannelNo");
					this.OnTvcHannelNoChanged();
				}
			}
		}
		
		[Column(Storage="_tvcHannelUrl", Name="TVChannelURL", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string TvcHannelUrl
		{
			get
			{
				return this._tvcHannelUrl;
			}
			set
			{
				if (((_tvcHannelUrl == value) 
							== false))
				{
					this.OnTvcHannelUrlChanging(value);
					this.SendPropertyChanging();
					this._tvcHannelUrl = value;
					this.SendPropertyChanged("TvcHannelUrl");
					this.OnTvcHannelUrlChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsWeatherCountry")]
	public partial class MsWeatherCountry : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		private string _weatherCountryDesc;
		
		private long _weatherCountryID;
		
		private string _weatherCountryName;
		
		private long _weatherRegionalID;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		
		partial void OnWeatherCountryDescChanged();
		
		partial void OnWeatherCountryDescChanging(string value);
		
		partial void OnWeatherCountryIDChanged();
		
		partial void OnWeatherCountryIDChanging(long value);
		
		partial void OnWeatherCountryNameChanged();
		
		partial void OnWeatherCountryNameChanging(string value);
		
		partial void OnWeatherRegionalIDChanged();
		
		partial void OnWeatherRegionalIDChanging(long value);
		#endregion
		
		
		public MsWeatherCountry()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		[Column(Storage="_weatherCountryDesc", Name="WeatherCountryDesc", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string WeatherCountryDesc
		{
			get
			{
				return this._weatherCountryDesc;
			}
			set
			{
				if (((_weatherCountryDesc == value) 
							== false))
				{
					this.OnWeatherCountryDescChanging(value);
					this.SendPropertyChanging();
					this._weatherCountryDesc = value;
					this.SendPropertyChanged("WeatherCountryDesc");
					this.OnWeatherCountryDescChanged();
				}
			}
		}
		
		[Column(Storage="_weatherCountryID", Name="WeatherCountryID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long WeatherCountryID
		{
			get
			{
				return this._weatherCountryID;
			}
			set
			{
				if ((_weatherCountryID != value))
				{
					this.OnWeatherCountryIDChanging(value);
					this.SendPropertyChanging();
					this._weatherCountryID = value;
					this.SendPropertyChanged("WeatherCountryID");
					this.OnWeatherCountryIDChanged();
				}
			}
		}
		
		[Column(Storage="_weatherCountryName", Name="WeatherCountryName", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string WeatherCountryName
		{
			get
			{
				return this._weatherCountryName;
			}
			set
			{
				if (((_weatherCountryName == value) 
							== false))
				{
					this.OnWeatherCountryNameChanging(value);
					this.SendPropertyChanging();
					this._weatherCountryName = value;
					this.SendPropertyChanged("WeatherCountryName");
					this.OnWeatherCountryNameChanged();
				}
			}
		}
		
		[Column(Storage="_weatherRegionalID", Name="WeatherRegionalID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long WeatherRegionalID
		{
			get
			{
				return this._weatherRegionalID;
			}
			set
			{
				if ((_weatherRegionalID != value))
				{
					this.OnWeatherRegionalIDChanging(value);
					this.SendPropertyChanging();
					this._weatherRegionalID = value;
					this.SendPropertyChanged("WeatherRegionalID");
					this.OnWeatherRegionalIDChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsWeatherLocation")]
	public partial class MsWeatherLocation : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private int _rowStatus;
		
		private string _rssUrl;
		
		private System.DateTime _timestamp;
		
		private long _weatherCountryID;
		
		private string _weatherLoationDesc;
		
		private System.Nullable<System.DateTime> _weatherLocationDate;
		
		private long _weatherLocationID;
		
		private string _weatherLocationName;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnRssUrlChanged();
		
		partial void OnRssUrlChanging(string value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		
		partial void OnWeatherCountryIDChanged();
		
		partial void OnWeatherCountryIDChanging(long value);
		
		partial void OnWeatherLoationDescChanged();
		
		partial void OnWeatherLoationDescChanging(string value);
		
		partial void OnWeatherLocationDateChanged();
		
		partial void OnWeatherLocationDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnWeatherLocationIDChanged();
		
		partial void OnWeatherLocationIDChanging(long value);
		
		partial void OnWeatherLocationNameChanged();
		
		partial void OnWeatherLocationNameChanging(string value);
		#endregion
		
		
		public MsWeatherLocation()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_rssUrl", Name="RssURL", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string RssUrl
		{
			get
			{
				return this._rssUrl;
			}
			set
			{
				if (((_rssUrl == value) 
							== false))
				{
					this.OnRssUrlChanging(value);
					this.SendPropertyChanging();
					this._rssUrl = value;
					this.SendPropertyChanged("RssUrl");
					this.OnRssUrlChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		[Column(Storage="_weatherCountryID", Name="WeatherCountryID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long WeatherCountryID
		{
			get
			{
				return this._weatherCountryID;
			}
			set
			{
				if ((_weatherCountryID != value))
				{
					this.OnWeatherCountryIDChanging(value);
					this.SendPropertyChanging();
					this._weatherCountryID = value;
					this.SendPropertyChanged("WeatherCountryID");
					this.OnWeatherCountryIDChanged();
				}
			}
		}
		
		[Column(Storage="_weatherLoationDesc", Name="WeatherLoationDesc", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string WeatherLoationDesc
		{
			get
			{
				return this._weatherLoationDesc;
			}
			set
			{
				if (((_weatherLoationDesc == value) 
							== false))
				{
					this.OnWeatherLoationDescChanging(value);
					this.SendPropertyChanging();
					this._weatherLoationDesc = value;
					this.SendPropertyChanged("WeatherLoationDesc");
					this.OnWeatherLoationDescChanged();
				}
			}
		}
		
		[Column(Storage="_weatherLocationDate", Name="WeatherLocationDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> WeatherLocationDate
		{
			get
			{
				return this._weatherLocationDate;
			}
			set
			{
				if ((_weatherLocationDate != value))
				{
					this.OnWeatherLocationDateChanging(value);
					this.SendPropertyChanging();
					this._weatherLocationDate = value;
					this.SendPropertyChanged("WeatherLocationDate");
					this.OnWeatherLocationDateChanged();
				}
			}
		}
		
		[Column(Storage="_weatherLocationID", Name="WeatherLocationID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long WeatherLocationID
		{
			get
			{
				return this._weatherLocationID;
			}
			set
			{
				if ((_weatherLocationID != value))
				{
					this.OnWeatherLocationIDChanging(value);
					this.SendPropertyChanging();
					this._weatherLocationID = value;
					this.SendPropertyChanged("WeatherLocationID");
					this.OnWeatherLocationIDChanged();
				}
			}
		}
		
		[Column(Storage="_weatherLocationName", Name="WeatherLocationName", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string WeatherLocationName
		{
			get
			{
				return this._weatherLocationName;
			}
			set
			{
				if (((_weatherLocationName == value) 
							== false))
				{
					this.OnWeatherLocationNameChanging(value);
					this.SendPropertyChanging();
					this._weatherLocationName = value;
					this.SendPropertyChanged("WeatherLocationName");
					this.OnWeatherLocationNameChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.MsWeatherRegion")]
	public partial class MsWeatherRegion : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _imageFile;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		private string _weatherRegionDesc;
		
		private long _weatherRegionID;
		
		private string _weatherRegionName;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnImageFileChanged();
		
		partial void OnImageFileChanging(string value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		
		partial void OnWeatherRegionDescChanged();
		
		partial void OnWeatherRegionDescChanging(string value);
		
		partial void OnWeatherRegionIDChanged();
		
		partial void OnWeatherRegionIDChanging(long value);
		
		partial void OnWeatherRegionNameChanged();
		
		partial void OnWeatherRegionNameChanging(string value);
		#endregion
		
		
		public MsWeatherRegion()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_imageFile", Name="ImageFile", DbType="varchar(150)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageFile
		{
			get
			{
				return this._imageFile;
			}
			set
			{
				if (((_imageFile == value) 
							== false))
				{
					this.OnImageFileChanging(value);
					this.SendPropertyChanging();
					this._imageFile = value;
					this.SendPropertyChanged("ImageFile");
					this.OnImageFileChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		[Column(Storage="_weatherRegionDesc", Name="WeatherRegionDesc", DbType="text", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string WeatherRegionDesc
		{
			get
			{
				return this._weatherRegionDesc;
			}
			set
			{
				if (((_weatherRegionDesc == value) 
							== false))
				{
					this.OnWeatherRegionDescChanging(value);
					this.SendPropertyChanging();
					this._weatherRegionDesc = value;
					this.SendPropertyChanged("WeatherRegionDesc");
					this.OnWeatherRegionDescChanged();
				}
			}
		}
		
		[Column(Storage="_weatherRegionID", Name="WeatherRegionID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long WeatherRegionID
		{
			get
			{
				return this._weatherRegionID;
			}
			set
			{
				if ((_weatherRegionID != value))
				{
					this.OnWeatherRegionIDChanging(value);
					this.SendPropertyChanging();
					this._weatherRegionID = value;
					this.SendPropertyChanged("WeatherRegionID");
					this.OnWeatherRegionIDChanged();
				}
			}
		}
		
		[Column(Storage="_weatherRegionName", Name="WeatherRegionName", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string WeatherRegionName
		{
			get
			{
				return this._weatherRegionName;
			}
			set
			{
				if (((_weatherRegionName == value) 
							== false))
				{
					this.OnWeatherRegionNameChanging(value);
					this.SendPropertyChanging();
					this._weatherRegionName = value;
					this.SendPropertyChanged("WeatherRegionName");
					this.OnWeatherRegionNameChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.TrBillingRoom")]
	public partial class TRBillingRoom : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private long _billRoomID;
		
		private string _createdBy;
		
		private System.Nullable<System.DateTime> _createdDate;
		
		private string _modifiedBy;
		
		private System.Nullable<System.DateTime> _modifiedDate;
		
		private long _roomID;
		
		private string _roomNo;
		
		private string _roomTerminalIp;
		
		private System.Nullable<sbyte> _roomTvType;
		
		private System.Nullable<sbyte> _roomType;
		
		private int _rowStatus;
		
		private System.DateTime _timestamp;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnBillRoomIDChanged();
		
		partial void OnBillRoomIDChanging(long value);
		
		partial void OnCreatedByChanged();
		
		partial void OnCreatedByChanging(string value);
		
		partial void OnCreatedDateChanged();
		
		partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnModifiedByChanged();
		
		partial void OnModifiedByChanging(string value);
		
		partial void OnModifiedDateChanged();
		
		partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnRoomIDChanged();
		
		partial void OnRoomIDChanging(long value);
		
		partial void OnRoomNoChanged();
		
		partial void OnRoomNoChanging(string value);
		
		partial void OnRoomTerminalIpChanged();
		
		partial void OnRoomTerminalIpChanging(string value);
		
		partial void OnRoomTvTypeChanged();
		
		partial void OnRoomTvTypeChanging(System.Nullable<sbyte> value);
		
		partial void OnRoomTypeChanged();
		
		partial void OnRoomTypeChanging(System.Nullable<sbyte> value);
		
		partial void OnRowStatusChanged();
		
		partial void OnRowStatusChanging(int value);
		
		partial void OnTimestampChanged();
		
		partial void OnTimestampChanging(System.DateTime value);
		#endregion
		
		
		public TRBillingRoom()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_billRoomID", Name="BillRoomID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long BillRoomID
		{
			get
			{
				return this._billRoomID;
			}
			set
			{
				if ((_billRoomID != value))
				{
					this.OnBillRoomIDChanging(value);
					this.SendPropertyChanging();
					this._billRoomID = value;
					this.SendPropertyChanged("BillRoomID");
					this.OnBillRoomIDChanged();
				}
			}
		}
		
		[Column(Storage="_createdBy", Name="CreatedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CreatedBy
		{
			get
			{
				return this._createdBy;
			}
			set
			{
				if (((_createdBy == value) 
							== false))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._createdBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[Column(Storage="_createdDate", Name="CreatedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				if ((_createdDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._createdDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedBy", Name="ModifiedBy", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ModifiedBy
		{
			get
			{
				return this._modifiedBy;
			}
			set
			{
				if (((_modifiedBy == value) 
							== false))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._modifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[Column(Storage="_modifiedDate", Name="ModifiedDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				if ((_modifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._modifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[Column(Storage="_roomID", Name="RoomID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long RoomID
		{
			get
			{
				return this._roomID;
			}
			set
			{
				if ((_roomID != value))
				{
					this.OnRoomIDChanging(value);
					this.SendPropertyChanging();
					this._roomID = value;
					this.SendPropertyChanged("RoomID");
					this.OnRoomIDChanged();
				}
			}
		}
		
		[Column(Storage="_roomNo", Name="RoomNo", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string RoomNo
		{
			get
			{
				return this._roomNo;
			}
			set
			{
				if (((_roomNo == value) 
							== false))
				{
					this.OnRoomNoChanging(value);
					this.SendPropertyChanging();
					this._roomNo = value;
					this.SendPropertyChanged("RoomNo");
					this.OnRoomNoChanged();
				}
			}
		}
		
		[Column(Storage="_roomTerminalIp", Name="RoomTerminalIP", DbType="varchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string RoomTerminalIp
		{
			get
			{
				return this._roomTerminalIp;
			}
			set
			{
				if (((_roomTerminalIp == value) 
							== false))
				{
					this.OnRoomTerminalIpChanging(value);
					this.SendPropertyChanging();
					this._roomTerminalIp = value;
					this.SendPropertyChanged("RoomTerminalIp");
					this.OnRoomTerminalIpChanged();
				}
			}
		}
		
		[Column(Storage="_roomTvType", Name="RoomTvType", DbType="tinyint(4)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<sbyte> RoomTvType
		{
			get
			{
				return this._roomTvType;
			}
			set
			{
				if ((_roomTvType != value))
				{
					this.OnRoomTvTypeChanging(value);
					this.SendPropertyChanging();
					this._roomTvType = value;
					this.SendPropertyChanged("RoomTvType");
					this.OnRoomTvTypeChanged();
				}
			}
		}
		
		[Column(Storage="_roomType", Name="RoomType", DbType="tinyint(4)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<sbyte> RoomType
		{
			get
			{
				return this._roomType;
			}
			set
			{
				if ((_roomType != value))
				{
					this.OnRoomTypeChanging(value);
					this.SendPropertyChanging();
					this._roomType = value;
					this.SendPropertyChanged("RoomType");
					this.OnRoomTypeChanged();
				}
			}
		}
		
		[Column(Storage="_rowStatus", Name="RowStatus", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RowStatus
		{
			get
			{
				return this._rowStatus;
			}
			set
			{
				if ((_rowStatus != value))
				{
					this.OnRowStatusChanging(value);
					this.SendPropertyChanging();
					this._rowStatus = value;
					this.SendPropertyChanged("RowStatus");
					this.OnRowStatusChanged();
				}
			}
		}
		
		[Column(Storage="_timestamp", Name="Timestamp", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if ((_timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.USR_MsRoles")]
	public partial class UsRMSRoles : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _loweredRoleName;
		
		private string _roleName;
		
		private bool _systemRole;
		
		private long _usrmSrOlesID;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnLoweredRoleNameChanged();
		
		partial void OnLoweredRoleNameChanging(string value);
		
		partial void OnRoleNameChanged();
		
		partial void OnRoleNameChanging(string value);
		
		partial void OnSystemRoleChanged();
		
		partial void OnSystemRoleChanging(bool value);
		
		partial void OnUsrmSRolesIDChanged();
		
		partial void OnUsrmSRolesIDChanging(long value);
		#endregion
		
		
		public UsRMSRoles()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_loweredRoleName", Name="LoweredRoleName", DbType="varchar(200)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string LoweredRoleName
		{
			get
			{
				return this._loweredRoleName;
			}
			set
			{
				if (((_loweredRoleName == value) 
							== false))
				{
					this.OnLoweredRoleNameChanging(value);
					this.SendPropertyChanging();
					this._loweredRoleName = value;
					this.SendPropertyChanged("LoweredRoleName");
					this.OnLoweredRoleNameChanged();
				}
			}
		}
		
		[Column(Storage="_roleName", Name="RoleName", DbType="varchar(200)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string RoleName
		{
			get
			{
				return this._roleName;
			}
			set
			{
				if (((_roleName == value) 
							== false))
				{
					this.OnRoleNameChanging(value);
					this.SendPropertyChanging();
					this._roleName = value;
					this.SendPropertyChanged("RoleName");
					this.OnRoleNameChanged();
				}
			}
		}
		
		[Column(Storage="_systemRole", Name="SystemRole", DbType="bit(1)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public bool SystemRole
		{
			get
			{
				return this._systemRole;
			}
			set
			{
				if ((_systemRole != value))
				{
					this.OnSystemRoleChanging(value);
					this.SendPropertyChanging();
					this._systemRole = value;
					this.SendPropertyChanged("SystemRole");
					this.OnSystemRoleChanged();
				}
			}
		}
		
		[Column(Storage="_usrmSrOlesID", Name="USR_MsRolesID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long UsrmSRolesID
		{
			get
			{
				return this._usrmSrOlesID;
			}
			set
			{
				if ((_usrmSrOlesID != value))
				{
					this.OnUsrmSRolesIDChanging(value);
					this.SendPropertyChanging();
					this._usrmSrOlesID = value;
					this.SendPropertyChanged("UsrmSRolesID");
					this.OnUsrmSRolesIDChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.USRMsUsers")]
	public partial class UsRMSUsers : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private System.DateTime _createDate;
		
		private string _email;
		
		private int _failedPasswordAnswerCount;
		
		private int _failedPasswordAttemptCount;
		
		private bool _isApproved;
		
		private bool _isLockedOut;
		
		private System.DateTime _lastLockedOutDate;
		
		private System.DateTime _lastLoginDate;
		
		private string _loweredEmail;
		
		private string _loweredUserName;
		
		private string _password;
		
		private string _userName;
		
		private long _usrmSuSersID;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreateDateChanged();
		
		partial void OnCreateDateChanging(System.DateTime value);
		
		partial void OnEmailChanged();
		
		partial void OnEmailChanging(string value);
		
		partial void OnFailedPasswordAnswerCountChanged();
		
		partial void OnFailedPasswordAnswerCountChanging(int value);
		
		partial void OnFailedPasswordAttemptCountChanged();
		
		partial void OnFailedPasswordAttemptCountChanging(int value);
		
		partial void OnIsApprovedChanged();
		
		partial void OnIsApprovedChanging(bool value);
		
		partial void OnIsLockedOutChanged();
		
		partial void OnIsLockedOutChanging(bool value);
		
		partial void OnLastLockedOutDateChanged();
		
		partial void OnLastLockedOutDateChanging(System.DateTime value);
		
		partial void OnLastLoginDateChanged();
		
		partial void OnLastLoginDateChanging(System.DateTime value);
		
		partial void OnLoweredEmailChanged();
		
		partial void OnLoweredEmailChanging(string value);
		
		partial void OnLoweredUserNameChanged();
		
		partial void OnLoweredUserNameChanging(string value);
		
		partial void OnPasswordChanged();
		
		partial void OnPasswordChanging(string value);
		
		partial void OnUserNameChanged();
		
		partial void OnUserNameChanging(string value);
		
		partial void OnUsrmSUsersIDChanged();
		
		partial void OnUsrmSUsersIDChanging(long value);
		#endregion
		
		
		public UsRMSUsers()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createDate", Name="CreateDate", DbType="datetime", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime CreateDate
		{
			get
			{
				return this._createDate;
			}
			set
			{
				if ((_createDate != value))
				{
					this.OnCreateDateChanging(value);
					this.SendPropertyChanging();
					this._createDate = value;
					this.SendPropertyChanged("CreateDate");
					this.OnCreateDateChanged();
				}
			}
		}
		
		[Column(Storage="_email", Name="Email", DbType="varchar(200)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Email
		{
			get
			{
				return this._email;
			}
			set
			{
				if (((_email == value) 
							== false))
				{
					this.OnEmailChanging(value);
					this.SendPropertyChanging();
					this._email = value;
					this.SendPropertyChanged("Email");
					this.OnEmailChanged();
				}
			}
		}
		
		[Column(Storage="_failedPasswordAnswerCount", Name="FailedPasswordAnswerCount", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int FailedPasswordAnswerCount
		{
			get
			{
				return this._failedPasswordAnswerCount;
			}
			set
			{
				if ((_failedPasswordAnswerCount != value))
				{
					this.OnFailedPasswordAnswerCountChanging(value);
					this.SendPropertyChanging();
					this._failedPasswordAnswerCount = value;
					this.SendPropertyChanged("FailedPasswordAnswerCount");
					this.OnFailedPasswordAnswerCountChanged();
				}
			}
		}
		
		[Column(Storage="_failedPasswordAttemptCount", Name="FailedPasswordAttemptCount", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int FailedPasswordAttemptCount
		{
			get
			{
				return this._failedPasswordAttemptCount;
			}
			set
			{
				if ((_failedPasswordAttemptCount != value))
				{
					this.OnFailedPasswordAttemptCountChanging(value);
					this.SendPropertyChanging();
					this._failedPasswordAttemptCount = value;
					this.SendPropertyChanged("FailedPasswordAttemptCount");
					this.OnFailedPasswordAttemptCountChanged();
				}
			}
		}
		
		[Column(Storage="_isApproved", Name="IsApproved", DbType="bit(1)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public bool IsApproved
		{
			get
			{
				return this._isApproved;
			}
			set
			{
				if ((_isApproved != value))
				{
					this.OnIsApprovedChanging(value);
					this.SendPropertyChanging();
					this._isApproved = value;
					this.SendPropertyChanged("IsApproved");
					this.OnIsApprovedChanged();
				}
			}
		}
		
		[Column(Storage="_isLockedOut", Name="IsLockedOut", DbType="bit(1)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public bool IsLockedOut
		{
			get
			{
				return this._isLockedOut;
			}
			set
			{
				if ((_isLockedOut != value))
				{
					this.OnIsLockedOutChanging(value);
					this.SendPropertyChanging();
					this._isLockedOut = value;
					this.SendPropertyChanged("IsLockedOut");
					this.OnIsLockedOutChanged();
				}
			}
		}
		
		[Column(Storage="_lastLockedOutDate", Name="LastLockedOutDate", DbType="datetime", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime LastLockedOutDate
		{
			get
			{
				return this._lastLockedOutDate;
			}
			set
			{
				if ((_lastLockedOutDate != value))
				{
					this.OnLastLockedOutDateChanging(value);
					this.SendPropertyChanging();
					this._lastLockedOutDate = value;
					this.SendPropertyChanged("LastLockedOutDate");
					this.OnLastLockedOutDateChanged();
				}
			}
		}
		
		[Column(Storage="_lastLoginDate", Name="LastLoginDate", DbType="datetime", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime LastLoginDate
		{
			get
			{
				return this._lastLoginDate;
			}
			set
			{
				if ((_lastLoginDate != value))
				{
					this.OnLastLoginDateChanging(value);
					this.SendPropertyChanging();
					this._lastLoginDate = value;
					this.SendPropertyChanged("LastLoginDate");
					this.OnLastLoginDateChanged();
				}
			}
		}
		
		[Column(Storage="_loweredEmail", Name="LoweredEmail", DbType="varchar(200)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string LoweredEmail
		{
			get
			{
				return this._loweredEmail;
			}
			set
			{
				if (((_loweredEmail == value) 
							== false))
				{
					this.OnLoweredEmailChanging(value);
					this.SendPropertyChanging();
					this._loweredEmail = value;
					this.SendPropertyChanged("LoweredEmail");
					this.OnLoweredEmailChanged();
				}
			}
		}
		
		[Column(Storage="_loweredUserName", Name="LoweredUserName", DbType="varchar(200)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string LoweredUserName
		{
			get
			{
				return this._loweredUserName;
			}
			set
			{
				if (((_loweredUserName == value) 
							== false))
				{
					this.OnLoweredUserNameChanging(value);
					this.SendPropertyChanging();
					this._loweredUserName = value;
					this.SendPropertyChanged("LoweredUserName");
					this.OnLoweredUserNameChanged();
				}
			}
		}
		
		[Column(Storage="_password", Name="Password", DbType="varchar(200)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Password
		{
			get
			{
				return this._password;
			}
			set
			{
				if (((_password == value) 
							== false))
				{
					this.OnPasswordChanging(value);
					this.SendPropertyChanging();
					this._password = value;
					this.SendPropertyChanged("Password");
					this.OnPasswordChanged();
				}
			}
		}
		
		[Column(Storage="_userName", Name="UserName", DbType="varchar(200)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string UserName
		{
			get
			{
				return this._userName;
			}
			set
			{
				if (((_userName == value) 
							== false))
				{
					this.OnUserNameChanging(value);
					this.SendPropertyChanging();
					this._userName = value;
					this.SendPropertyChanged("UserName");
					this.OnUserNameChanged();
				}
			}
		}
		
		[Column(Storage="_usrmSuSersID", Name="USR_MsUsersID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long UsrmSUsersID
		{
			get
			{
				return this._usrmSuSersID;
			}
			set
			{
				if ((_usrmSuSersID != value))
				{
					this.OnUsrmSUsersIDChanging(value);
					this.SendPropertyChanging();
					this._usrmSuSersID = value;
					this.SendPropertyChanged("UsrmSUsersID");
					this.OnUsrmSUsersIDChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ITVDB.USR_UsersInRoles")]
	public partial class UsRuSERSInRoles : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private long _usrmSrOlesID;
		
		private long _usrmSuSersID;
		
		private long _usruSersInRolesID;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnUsrmSRolesIDChanged();
		
		partial void OnUsrmSRolesIDChanging(long value);
		
		partial void OnUsrmSUsersIDChanged();
		
		partial void OnUsrmSUsersIDChanging(long value);
		
		partial void OnUsruSersInRolesIDChanged();
		
		partial void OnUsruSersInRolesIDChanging(long value);
		#endregion
		
		
		public UsRuSERSInRoles()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_usrmSrOlesID", Name="USR_MsRolesID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long UsrmSRolesID
		{
			get
			{
				return this._usrmSrOlesID;
			}
			set
			{
				if ((_usrmSrOlesID != value))
				{
					this.OnUsrmSRolesIDChanging(value);
					this.SendPropertyChanging();
					this._usrmSrOlesID = value;
					this.SendPropertyChanged("UsrmSRolesID");
					this.OnUsrmSRolesIDChanged();
				}
			}
		}
		
		[Column(Storage="_usrmSuSersID", Name="USR_MsUsersID", DbType="bigint(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long UsrmSUsersID
		{
			get
			{
				return this._usrmSuSersID;
			}
			set
			{
				if ((_usrmSuSersID != value))
				{
					this.OnUsrmSUsersIDChanging(value);
					this.SendPropertyChanging();
					this._usrmSuSersID = value;
					this.SendPropertyChanged("UsrmSUsersID");
					this.OnUsrmSUsersIDChanged();
				}
			}
		}
		
		[Column(Storage="_usruSersInRolesID", Name="USR_UsersInRolesID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public long UsruSersInRolesID
		{
			get
			{
				return this._usruSersInRolesID;
			}
			set
			{
				if ((_usruSersInRolesID != value))
				{
					this.OnUsruSersInRolesIDChanging(value);
					this.SendPropertyChanging();
					this._usruSersInRolesID = value;
					this.SendPropertyChanged("UsruSersInRolesID");
					this.OnUsruSersInRolesIDChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
