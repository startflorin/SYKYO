using SystemInterface;

namespace DiagramCreator
{
    class Diagram
    {
        private static string sourceFileName = "HelloWorld";

        private string applicationPath =
            "C:\\Users\\PROFIMEDICA\\Documents\\Visual Studio 2010\\WindowsFormsApplication1\\DiagramCreator\\";
        private const string sourcePath = "DiagramFiles\\";
        private const string FilePath = "C:\\Program Files (x86)\\Graphviz 2.28\\bin\\";
        //private const string ProcessName = "dot.exe";
        private const string ProcessName = "gvedit.exe";
        private static string Arguments = "";
        readonly ExternalProcess GraphViz;

        public Diagram(string soucreFileName)
        {
            sourceFileName = soucreFileName;
            //Arguments = "\"" + applicationPath + sourcePath + sourceFileName + ".gv\" " + "-Tpng > \"" + applicationPath + sourcePath + "hello.png\"";
            Arguments = "\"" + applicationPath + sourcePath + sourceFileName + ".gv\" ";
            GraphViz = new ExternalProcess(FilePath, ProcessName, Arguments);
        }
        public Diagram()
        {
            sourceFileName = "default";
            //Arguments = "\"" + applicationPath + sourcePath + sourceFileName + ".gv\" " + "-Tpng > \"" + applicationPath + sourcePath + "hello.png\"";
            Arguments = "\"" + applicationPath + sourcePath + sourceFileName + ".gv\" ";
            GraphViz = new ExternalProcess(FilePath, ProcessName, Arguments);
        }

        public string ExecuteDiagram()
        {
            return GraphViz.Execute("");
        }
    }
}
