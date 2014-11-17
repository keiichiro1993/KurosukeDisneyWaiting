using System;

using Mindscape.LightSpeed;
using Mindscape.LightSpeed.Validation;
using Mindscape.LightSpeed.Linq;

namespace Common.Models
{
  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [System.ComponentModel.DataObject]
  [Table(IdentityMethod=IdentityMethod.KeyTable)]
  public partial class Theme : Entity<int>
  {
    #region Fields
  
    [ValidatePresence]
    private string _themeName;
    private int _parkId;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the ThemeName entity attribute.</summary>
    public const string ThemeNameField = "ThemeName";
    /// <summary>Identifies the ParkId entity attribute.</summary>
    public const string ParkIdField = "ParkId";


    #endregion
    
    #region Relationships

    [ReverseAssociation("Theme")]
    private readonly EntityCollection<Attraction> _attractions = new EntityCollection<Attraction>();
    [ReverseAssociation("Themes")]
    private readonly EntityHolder<Park> _park = new EntityHolder<Park>();


    #endregion
    
    #region Properties

    [System.Diagnostics.DebuggerNonUserCode]
    public EntityCollection<Attraction> Attractions
    {
      get { return Get(_attractions); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public Park Park
    {
      get { return Get(_park); }
      set { Set(_park, value); }
    }


    [System.Diagnostics.DebuggerNonUserCode]
    public string ThemeName
    {
      get { return Get(ref _themeName, "ThemeName"); }
      set { Set(ref _themeName, value, "ThemeName"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="Park" /> property.</summary>
    [System.Diagnostics.DebuggerNonUserCode]
    public int ParkId
    {
      get { return Get(ref _parkId, "ParkId"); }
      set { Set(ref _parkId, value, "ParkId"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [System.ComponentModel.DataObject]
  [Table(IdentityMethod=IdentityMethod.KeyTable)]
  public partial class Status : Entity<int>
  {
    #region Fields
  
    [ValidatePresence]
    private System.DateTime _updateDateTime;
    [ValidatePresence]
    private string _updateString;
    [ValidatePresence]
    private System.Nullable<int> _waitTime;
    private bool _run;
    private string _runString;
    private int _attractionId;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the UpdateDateTime entity attribute.</summary>
    public const string UpdateDateTimeField = "UpdateDateTime";
    /// <summary>Identifies the UpdateString entity attribute.</summary>
    public const string UpdateStringField = "UpdateString";
    /// <summary>Identifies the WaitTime entity attribute.</summary>
    public const string WaitTimeField = "WaitTime";
    /// <summary>Identifies the Run entity attribute.</summary>
    public const string RunField = "Run";
    /// <summary>Identifies the RunString entity attribute.</summary>
    public const string RunStringField = "RunString";
    /// <summary>Identifies the AttractionId entity attribute.</summary>
    public const string AttractionIdField = "AttractionId";


    #endregion
    
    #region Relationships

    [ReverseAssociation("Statuses")]
    private readonly EntityHolder<Attraction> _attraction = new EntityHolder<Attraction>();


    #endregion
    
    #region Properties

    [System.Diagnostics.DebuggerNonUserCode]
    public Attraction Attraction
    {
      get { return Get(_attraction); }
      set { Set(_attraction, value); }
    }


    [System.Diagnostics.DebuggerNonUserCode]
    public System.DateTime UpdateDateTime
    {
      get { return Get(ref _updateDateTime, "UpdateDateTime"); }
      set { Set(ref _updateDateTime, value, "UpdateDateTime"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string UpdateString
    {
      get { return Get(ref _updateString, "UpdateString"); }
      set { Set(ref _updateString, value, "UpdateString"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public System.Nullable<int> WaitTime
    {
      get { return Get(ref _waitTime, "WaitTime"); }
      set { Set(ref _waitTime, value, "WaitTime"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public bool Run
    {
      get { return Get(ref _run, "Run"); }
      set { Set(ref _run, value, "Run"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string RunString
    {
      get { return Get(ref _runString, "RunString"); }
      set { Set(ref _runString, value, "RunString"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="Attraction" /> property.</summary>
    [System.Diagnostics.DebuggerNonUserCode]
    public int AttractionId
    {
      get { return Get(ref _attractionId, "AttractionId"); }
      set { Set(ref _attractionId, value, "AttractionId"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [System.ComponentModel.DataObject]
  [Table(IdentityMethod=IdentityMethod.KeyTable)]
  public partial class Park : Entity<int>
  {
    #region Fields
  
    [ValidatePresence]
    private string _waitingTimeUrl;
    [ValidatePresence]
    private string _parkName;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the WaitingTimeUrl entity attribute.</summary>
    public const string WaitingTimeUrlField = "WaitingTimeUrl";
    /// <summary>Identifies the ParkName entity attribute.</summary>
    public const string ParkNameField = "ParkName";


    #endregion
    
    #region Relationships

    [ReverseAssociation("Park")]
    private readonly EntityCollection<Theme> _themes = new EntityCollection<Theme>();


    #endregion
    
    #region Properties

    [System.Diagnostics.DebuggerNonUserCode]
    public EntityCollection<Theme> Themes
    {
      get { return Get(_themes); }
    }


    [System.Diagnostics.DebuggerNonUserCode]
    public string WaitingTimeUrl
    {
      get { return Get(ref _waitingTimeUrl, "WaitingTimeUrl"); }
      set { Set(ref _waitingTimeUrl, value, "WaitingTimeUrl"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string ParkName
    {
      get { return Get(ref _parkName, "ParkName"); }
      set { Set(ref _parkName, value, "ParkName"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [System.ComponentModel.DataObject]
  [Table(IdentityMethod=IdentityMethod.KeyTable)]
  public partial class Attraction : Entity<int>
  {
    #region Fields
  
    [ValidatePresence]
    private string _title;
    private string _description;
    private int _themeId;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the Title entity attribute.</summary>
    public const string TitleField = "Title";
    /// <summary>Identifies the Description entity attribute.</summary>
    public const string DescriptionField = "Description";
    /// <summary>Identifies the ThemeId entity attribute.</summary>
    public const string ThemeIdField = "ThemeId";


    #endregion
    
    #region Relationships

    [ReverseAssociation("Attraction")]
    private readonly EntityCollection<Status> _statuses = new EntityCollection<Status>();
    [ReverseAssociation("Attractions")]
    private readonly EntityHolder<Theme> _theme = new EntityHolder<Theme>();


    #endregion
    
    #region Properties

    [System.Diagnostics.DebuggerNonUserCode]
    public EntityCollection<Status> Statuses
    {
      get { return Get(_statuses); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public Theme Theme
    {
      get { return Get(_theme); }
      set { Set(_theme, value); }
    }


    [System.Diagnostics.DebuggerNonUserCode]
    public string Title
    {
      get { return Get(ref _title, "Title"); }
      set { Set(ref _title, value, "Title"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Description
    {
      get { return Get(ref _description, "Description"); }
      set { Set(ref _description, value, "Description"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="Theme" /> property.</summary>
    [System.Diagnostics.DebuggerNonUserCode]
    public int ThemeId
    {
      get { return Get(ref _themeId, "ThemeId"); }
      set { Set(ref _themeId, value, "ThemeId"); }
    }

    #endregion
  }




  /// <summary>
  /// Provides a strong-typed unit of work for working with the WaitingTimeModel model.
  /// </summary>
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  public partial class WaitingTimeModelUnitOfWork : Mindscape.LightSpeed.UnitOfWork
  {

    public System.Linq.IQueryable<Theme> Themes
    {
      get { return this.Query<Theme>(); }
    }
    
    public System.Linq.IQueryable<Status> Statuses
    {
      get { return this.Query<Status>(); }
    }
    
    public System.Linq.IQueryable<Park> Parks
    {
      get { return this.Query<Park>(); }
    }
    
    public System.Linq.IQueryable<Attraction> Attractions
    {
      get { return this.Query<Attraction>(); }
    }
    
  }

}
