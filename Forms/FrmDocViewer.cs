using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using SelfControls.Controls;

namespace MurliAnveshan
{
    public partial class FrmDocViewer : Form
    {
        MainForm mainFormInstance;

        private readonly string hindiAVMurlisPDFPath;

        public FrmDocViewer()
        {
            InitializeComponent();

            hindiAVMurlisPDFPath = ConfigurationManager.ConnectionStrings["HindiAVMurlisPDFPath"].ConnectionString;
        }

        public FrmDocViewer(MainForm mainFormInstance) : this()
        {
            this.mainFormInstance = mainFormInstance;
        }

        public FrmDocViewer(MainForm mainFormInstance, ResultDetails resultDetails) : this(mainFormInstance)
        {
            LoadThePDF(resultDetails);
        }

        public void LoadThePDF(ResultDetails resultDetails)
        {
            string fileNameWithoutExtenstion = resultDetails.FileName.Split('.').FirstOrDefault();
            string fileName = fileNameWithoutExtenstion + ".pdf";
            pdfViewer1.LoadPdf(Path.Combine(hindiAVMurlisPDFPath, fileName), fileName, resultDetails.ContentDate);
        }
    }
}
