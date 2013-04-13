
 In  CodeProcessor

	 a  FileAccess
		 => Can:  GetFilePathList
 as a method

		 => Can:  GetFileList
 as a method

		 => Can:  GetFileContent
 as a method



 In  CodeProcessor
		 =>   Form
 as a :
		 => Can:  Form1
 as a method





 In  CodeProcessor
	 a  Form1




 In  CodeProcessor
	 a  Program



 In  CodeProcessor
	 a  CodeReader
		 => Can:  ReadCodeFile
 as a method






		 => Can:  GetAllCodeElements
 as a method



	 a  CodeSection
		 =>   Visibility
 as a int

		 =>   ReturnType
 as a string

		 =>   Complexity
 as a int

		 =>   FilePath
 as a string

		 =>   EndSection
 as a int

		 => Can:  CodeSection
 as a method

		 => Can:  CodeSection
 as a method



 In  CodeProcessor
	 a  CodeWriter
		 => Can:  ReadCodeFile
 as a method






		 => Can:  GetAllCodeElements
 as a method

















 In  Async
		 => Can:  Form1
 as a method








 In  DeviceApplication1
		 =>   Form
 as a :
		 => Can:  FailDetails
 as a method

		 =>   Message
 as a string

		 =>   Trace
 as a string



 In  DeviceApplication1
	 a  FailDetails




 In  DeviceApplication1
		 =>   Form
 as a :
		 => Can:  MainForm
 as a method











 In  DeviceApplication1
	 a  MainForm




 In  DeviceApplication1
	 a  Program







 In  TableEditor
		 => Can:  Form1
 as a method











 In  DataModel
		 =>   Form
 as a :
		 => Can:  Form1
 as a method



 In  DataModel
	 a  Form1




 In  DataModel
	 a  Program



 In  DataModel.BL

	 a  DataModel
		 =>   rootCodeElements
 as a List<CodeElement>



 In  DataModel.DL.CodeEntity

	 a  CodeElement
		 =>   Name
 as a string

		 =>   Childs
 as a List<CodeElement>

		 =>   Visibility
 as a int

		 =>   ReturnType
 as a string

		 =>   Parameters
 as a List<string>



 In  DataModel.DL.CodeEntity
		 =>   CodeElement
 as a :
		 =>   ElementFile
 as a FileInfo

		 =>   ElementStartLine
 as a int

		 =>   ElementEndLine
 as a int

		 =>   ElementName
 as a string

		 =>   ElementAccessType
 as a DataAccessType

		 =>   IsStatic
 as a bool

		 =>   ElementNamespace
 as a ElementNamespace

		 =>   Parent
 as a ElementClass

		 =>   DefaultClass
 as a ElementClass



 In  DataModel.DL.CodeEntity
		 =>   CodeElement
 as a :
		 =>   ElementFile
 as a FileInfo

		 =>   Parameters
 as a List<string[]>

		 =>   ElementNamespaceName
 as a string

		 =>   Result
 as a string

		 =>   ElementClassName
 as a string

		 =>   ElementStartLine
 as a int

		 =>   ElementEndLine
 as a int

		 =>   ElementName
 as a string

		 =>   ElementClass
 as a ElementClass

		 =>   ElementAccessType
 as a DataAccessType

		 =>   IsStatic
 as a bool

		 => Can:  ToString
 as a method

		 =>   DefaultMethod
 as a ElementMethod



 In  DataModel.DL.CodeEntity
		 =>   CodeElement
 as a :
		 =>   ElementFile
 as a Collection<FileInfo>

		 =>   ElementStartLine
 as a Collection<int>

		 =>   ElementEndLine
 as a Collection<int>

		 =>   ElementName
 as a string

		 =>   DefaultNamespace
 as a ElementNamespace



 In  DataModel.DL.CodeEntity
		 =>   CodeElement
 as a :
		 =>   ElementName
 as a string







 In  DiagramCreator
		 => Can:  Diagram
 as a method

		 => Can:  Diagram
 as a method

		 => Can:  ExecuteDiagram
 as a method


 In  DiagramCreator
		 =>   Form
 as a :
		 => Can:  Form1
 as a method








 In  DiagramCreator
	 a  Form1




 In  DiagramCreator
	 a  Program







 In  DataPersistency
		 =>   Form
 as a :
		 => Can:  Form1
 as a method






 In  DataPersistency
	 a  Form1




 In  DataPersistency
	 a  Program



 In  DataPersistency.BL.UserOptions
		 =>   INotifyPropertyChanged
 as a :
		 =>   LogNumbersNone
 as a bool

		 =>   LogNumbersResults
 as a bool

		 =>   LogNumbersParameters
 as a bool

		 =>   LogNumbersCode
 as a bool

		 =>   LogObjectsNone
 as a bool

		 =>   LogObjectsResults
 as a bool

		 =>   LogObjectsParameters
 as a bool

		 =>   LogObjectsCode
 as a bool

		 =>   LogRelationsNone
 as a bool

		 =>   LogRelationsResults
 as a bool

		 =>   LogRelationsParameters
 as a bool

		 =>   LogRelationsCode
 as a bool

		 =>   LogLogicsNone
 as a bool

		 =>   LogLogicsResults
 as a bool

		 =>   LogLogicsParameters
 as a bool

		 =>   LogLogicsCode
 as a bool

		 =>   levelNumbers
 as a int

		 =>   levelObjects
 as a int

		 =>   levelRelations
 as a int

		 =>   levelLogics
 as a int




 In  DataPersistency.BL.UserOptions
	 a  UserOptions
		 =>   LogingSystemOptionsFile
 as a FileInfo

		 =>   LogingSystemTraceFile
 as a FileInfo

		 => Can:  GetFileInfo
 as a method



 In  DataPersistency.DL.FileAccess

	 a  FileAccess
		 => Can:  GetFilePathList
 as a method

		 => Can:  GetFileList
 as a method

		 => Can:  GetFileContent
 as a method



 In  DataPersistency.DL.FileAccess

	 a  TextFileAccess
		 => Can:  SaveModel
 as a method

		 => Can:  RestoreModel
 as a method



 In  DataPersistency.DL.CommenAccess.ObjectsFromNumbers

	 a  SymbolItem
		 => Can:  SymbolItem
 as a method

		 => Can:  SymbolItem
 as a method

		 => Can:  ToString
 as a method



		 =>   Null
 as a SymbolItem

		 =>   Unknown
 as a SymbolItem



 In  DataPersistency.DL.Logging

	 a  LoggingSystem
		 =>   LogAdministrativeTasks
 as a bool

		 =>   TraceCode
 as a bool

		 =>   LogMessage
 as a string

		 =>   BranchNo
 as a int

		 =>   BranchName
 as a string

		 =>   LogTrace
 as a List<string>

		 =>   LogMethod
 as a ElementMethod


		 =>   INotifyPropertyChanged
 as a :
		 =>   LogMessage
 as a string

		 =>   LogTrace
 as a List<string>

		 =>   BranchName
 as a string

		 =>   BranchNo
 as a int

		 =>   LogMethod
 as a ElementMethod




 In  DataPersistency.DL.ServerAccess

	 a  OperatorLocation
		 => Can:  ToString
 as a method



	 a  OperatorMultiplicity
		 => Can:  ToString
 as a method



	 a  OperatorID
		 => Can:  OperatorID
 as a method

		 => Can:  OperatorID
 as a method

		 => Can:  ToString
 as a method

		 =>   Exists
 as a bool

		 =>   hasMultiplicity
 as a bool



 In  DataPersistency.DL.ServerAccess
		 =>   ServerAccessInterface
 as a interface





		 =>   ServerAccessInterface
 as a :
		 =>   AcceptSymbols
 as a bool

		 =>   AcceptOperators
 as a bool

		 =>   AcceptRelations
 as a bool


		 => Can:  getConnectionState
 as a method

		 =>   DatabaseName
 as a string


		 => Can:  TryToOpenConnection
 as a method






		 => Can:  GetSymbolNamesByID
 as a method




		 => Can:  CreateSymbolByName
 as a method


		 => Can:  CreateSymbolAlias
 as a method




		 => Can:  GetAllOperators
 as a method

		 => Can:  GetOperatorNamesByID
 as a method


		 => Can:  GetContent
 as a method

		 => Can:  IsTransitive
 as a method

		 => Can:  CreateRelation
 as a method





		 =>   ServerAccessInterface
 as a :
		 =>   AcceptSymbols
 as a bool

		 =>   AcceptOperators
 as a bool

		 =>   AcceptRelations
 as a bool

		 => Can:  TryToOpenConnection
 as a method



		 => Can:  getConnectionState
 as a method

		 =>   DatabaseName
 as a string







		 => Can:  CreateSymbolAlias
 as a method



		 => Can:  getRecord
 as a method



		 => Can:  GetContent
 as a method

		 => Can:  GetOperatorsByName
 as a method

		 => Can:  GetAllOperators
 as a method

		 => Can:  CreateRelationAsNew
 as a method

		 => Can:  IsTransitive
 as a method


		 => Can:  CreateRelation
 as a method








 In  DataPersistency.UI.Logging
		 =>   Form
 as a :
		 => Can:  SQLView
 as a method








		 => Can:  Log
 as a method

		 => Can:  LogResult
 as a method

		 => Can:  LogHumanResult
 as a method

		 => Can:  LogResult
 as a method

		 => Can:  LogResult
 as a method

		 => Can:  LogResult
 as a method



 In  DataPersistency.UI.Logging
	 a  SQLView




 In  DataPersistency.UI.UserOptions
		 =>   Form
 as a :
		 => Can:  ConnectionStringEditor
 as a method




 In  DataPersistency.UI.UserOptions
	 a  ConnectionStringEditor




 In  DataPersistency.UI.UserOptions
	 a  DatabaseOptions




 In  DataPersistency.UI.UserOptions
		 =>   Form
 as a :
		 => Can:  TraceOptions
 as a method







 In  DataPersistency.UI.UserOptions
	 a  TraceOptions




 In  NaturalLanguageProcessor
	 a  Form1




 In  NaturalLanguageProcessor

	 a  NaturalWriter
		 => Can:  ConvertToNaturalLanguage
 as a method




 In  NaturalLanguageProcessor
	 a  Program







 In  NaturalLanguageProcessor.Test
		 =>   Form
 as a :
		 => Can:  InternalFromNatural
 as a method



 In  NaturalLanguageProcessor.Test
	 a  InternalFromNatural




 In  NaturalLanguageProcessor.Test
		 =>   Form
 as a :
		 => Can:  NaturalFromInternal
 as a method





 In  NaturalLanguageProcessor.Test
	 a  NaturalFromInternal




 In  ObjectCollision
		 =>   Form
 as a :
		 => Can:  Form1
 as a method



 In  ObjectCollision
	 a  Form1




 In  ObjectCollision
	 a  Program



 In  ObjectCollision.BL

	 a  CurrentStateDataSet





		 =>   global::System.Data.DataRow
 as a :

		 =>   global::System.Data.DataRow
 as a :





 In  ObjectCollision.BL.CurrentStateDataSetTableAdapters














 In  SystemInterface
		 =>   Form
 as a :
		 => Can:  Form1
 as a method



 In  SystemInterface
	 a  Form1




 In  SystemInterface
	 a  Program







 In  WindowsFormsApplication1
	 a  Program




		 =>   Oracle
 as a bool

		 =>   MySql
 as a bool

		 =>   SqlServer
 as a bool

		 =>   Postgree
 as a bool

		 =>   SqLite
 as a bool




 In  WindowsFormsApplication1.BL

	 a  DataSet1
		 => Can:  AttachTable
 as a method

		 => Can:  DataSet1
 as a method







 In  WindowsFormsApplication1
	 a  FileAccess
		 => Can:  GetFilePathList
 as a method

		 => Can:  GetFileList
 as a method



 In  WindowsFormsApplication1.DataAccess.FileAccess

	 a  TextFileAccess
		 => Can:  SaveModel
 as a method

		 => Can:  RestoreModel
 as a method



 In  WindowsFormsApplication1.Level_Logic_From_Relations
	 a  RelationItem

		 => Can:  ToString
 as a method



 In  WindowsFormsApplication1.BL
		 =>   ServerAccessInterface
 as a interface





		 =>   ServerAccessInterface
 as a :
		 =>   AcceptSymbols
 as a bool

		 =>   AcceptOperators
 as a bool

		 =>   AcceptRelations
 as a bool



		 => Can:  getConnectionState
 as a method

		 =>   DatabaseName
 as a string



		 => Can:  CreateSymbolAlias
 as a method






		 => Can:  getRecord
 as a method



		 => Can:  GetContent
 as a method

		 => Can:  GetOperatorsByName
 as a method

		 => Can:  GetAllOperators
 as a method

		 => Can:  CreateRelation
 as a method

		 => Can:  IsTransitive
 as a method


		 => Can:  CreateRelation2
 as a method




		 =>   AcceptSymbols
 as a bool

		 =>   AcceptOperators
 as a bool

		 =>   AcceptRelations
 as a bool



		 => Can:  getConnectionState
 as a method

		 =>   DatabaseName
 as a string








		 => Can:  CreateSymbolAlias
 as a method



		 => Can:  getRecord
 as a method



		 => Can:  GetContent
 as a method

		 => Can:  GetOperatorsByName
 as a method

		 => Can:  GetAllOperators
 as a method

		 => Can:  CreateRelationAsNew
 as a method

		 => Can:  IsTransitive
 as a method


		 => Can:  CreateRelation
 as a method





	 a  SymbolCollection

		 => Can:  SymbolCollection
 as a method

		 => Can:  SymbolCollection
 as a method





		 => Can:  Count
 as a method

		 => Can:  Count
 as a method

		 => Can:  Count
 as a method


		 => Can:  Exists
 as a method


		 => Can:  ToString
 as a method





 In  WindowsFormsApplication1.Level_Objects_From_Numbers

	 a  SymbolItem
		 => Can:  SymbolItem
 as a method

		 => Can:  ToString
 as a method



		 =>   Null
 as a SymbolItem

		 =>   Unknown
 as a SymbolItem



 In  WindowsFormsApplication1.Level_Operator_From_Numbers
	 a  OperatorCollection

		 => Can:  SolveRelation
 as a method









		 => Can:  InRelation
 as a method


		 => Can:  SetRelation
 as a method










 In  WindowsFormsApplication1.Level_Operator_From_Numbers
	 a  OperatorItem
		 => Can:  OperatorItem
 as a method

		 => Can:  ToString
 as a method




 In  WindowsFormsApplication1.CD
	 a  DecodePhrase


		 => Can:  Decode
 as a method



 In  WindowsFormsApplication1
	 a  Symbol
		 =>   SymbolID
 as a int[]

		 =>   Name
 as a string

		 => Can:  Symbol
 as a method

		 => Can:  Symbol
 as a method




		 => Can:  RegisterSymbolIfNotExists
 as a method





		 =>   CreateUnknown
 as a bool



 In  WindowsFormsApplication1.Model.FileSystem
	 a  UserOptions
		 =>   LogingSystemOptionsFile
 as a FileInfo

		 =>   LogingSystemTraceFile
 as a FileInfo

		 => Can:  GetFileInfo
 as a method



 In  WindowsFormsApplication1.DataAccess.Options
		 =>   INotifyPropertyChanged
 as a :
		 =>   LogNumbersNone
 as a bool

		 =>   LogNumbersResults
 as a bool

		 =>   LogNumbersParameters
 as a bool

		 =>   LogNumbersCode
 as a bool

		 =>   LogObjectsNone
 as a bool

		 =>   LogObjectsResults
 as a bool

		 =>   LogObjectsParameters
 as a bool

		 =>   LogObjectsCode
 as a bool

		 =>   LogRelationsNone
 as a bool

		 =>   LogRelationsResults
 as a bool

		 =>   LogRelationsParameters
 as a bool

		 =>   LogRelationsCode
 as a bool

		 =>   LogLogicsNone
 as a bool

		 =>   LogLogicsResults
 as a bool

		 =>   LogLogicsParameters
 as a bool

		 =>   LogLogicsCode
 as a bool

		 =>   levelNumbers
 as a int

		 =>   levelObjects
 as a int

		 =>   levelRelations
 as a int

		 =>   levelLogics
 as a int













		 =>   global::System.Data.DataRow
 as a :

		 =>   global::System.Data.DataRow
 as a :

		 =>   global::System.Data.DataRow
 as a :

		 =>   global::System.Data.DataRow
 as a :

		 =>   global::System.Data.DataRow
 as a :








 In  WindowsFormsApplication1.UI
		 =>   Form
 as a :




 In  WindowsFormsApplication1.UI
	 a  CustomMessageBox




		 =>   Form
 as a :




		 => Can:  EvaluateInput
 as a method

		 => Can:  PrepareInput
 as a method



























 In  WindowsFormsApplication1
	 a  Form1




 In  WindowsFormsApplication1.UI
	 a  MSDNReader




 In  WindowsFormsApplication1
		 =>   Form
 as a :
		 => Can:  MyCode
 as a method




 In  WindowsFormsApplication1
	 a  MyCode




 In  WindowsFormsApplication1.DL
		 =>   INotifyPropertyChanged
 as a :
		 =>   MyName
 as a string

		 => Can:  Parameter
 as a method


		 =>   Location
 as a object



 In  WindowsFormsApplication1.UI
		 =>   Form
 as a :
		 =>   ActualID
 as a int[]





		 => Can:  AddParameter
 as a method







 In  WindowsFormsApplication1.UI
	 a  WatchParameters




 In  WindowsFormsApplication1
	 a  WebView




 In  WindowsFormsApplication1.UI
		 =>   Form
 as a :
		 => Can:  DatabaseOptions
 as a method









 In  WindowsFormsApplication1.UI
	 a  DatabaseOptions




 In  WindowsFormsApplication1.UI
		 =>   Form
 as a :
		 => Can:  TraceOptions
 as a method





 In  WindowsFormsApplication1.UI
	 a  TraceOptions




 In  WindowsFormsApplication1.UI
	 a  TestRelation




 In  WindowsFormsApplication1.UI
		 =>   Form
 as a :
		 => Can:  TestSentence
 as a method



 In  WindowsFormsApplication1.UI
	 a  TestSentence




 In  WindowsFormsApplication1.UI
		 =>   Form
 as a :










 In  WindowsFormsApplication1.UI
	 a  TestServer




 In  WindowsFormsApplication1.UI
		 =>   Form
 as a :
		 => Can:  TestSymbol
 as a method






 In  WindowsFormsApplication1.UI
	 a  TestSymbol






