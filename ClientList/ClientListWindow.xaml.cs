using ZooMail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xceed.Words.NET;
using Xceed.Document.NET;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using ZooMail.PersonalAccounting;

namespace ZooMail
{
    /// <summary>
    /// Логика взаимодействия для ClientListWindow.xaml
    /// </summary>
    public partial class ClientListWindow : Window
    {
        public ClientListWindow()
        {
            InitializeComponent();
        }

        List<Models.ModelClientDetail> listClient;
        public string extension = string.Empty;
        private void updateData()
        {
            listClient = (new DBManager()).getClientListDetail();
            dataGridClients.ItemsSource = listClient;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            updateData();
        }

        private void ButtonDetail_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = dataGridClients.SelectedIndex;
            if (selectedIndex < 0)
            {
                MessageBox.Show("Не выбран клиент");
                return;
            }



            var ap = new DBManager().getClientList();

            Models.ModelClient modelClient = null;
            foreach (var it in ap)
            {
                if (it.ID_Authorization == listClient[selectedIndex].ID_Authorization)
                {
                    modelClient = it;
                    break;
                }
            }

            var ce = new RegistrationWindow(modelClient, true);
            ce.ShowDialog();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void createExportDoc()
        {
            try
            {


                DBManager con = new DBManager();

                var modelClientDetail = con.getClientListDetail();

                if (extension == string.Empty)
                {
                    MessageBox.Show("Не выбран тип экспортруемого файла");
                    return;
                }


                switch (extension)
                {
                    case (".docx"):
                        string pathDocumentDOCX = Session.baseDir + "Список клиентов" + extension;
                        DocX document = DocX.Create(pathDocumentDOCX);
                        Xceed.Document.NET.Paragraph paragraph = document.InsertParagraph();
                        paragraph.
                            AppendLine("Документ '" + "Отчет список клиентов" + "' создан " + DateTime.Now.ToShortDateString()).
                            Font("Time New Roman").
                            FontSize(16).Bold().Alignment = Alignment.left;

                        paragraph.AppendLine();
                        Xceed.Document.NET.Table doctable = document.AddTable(modelClientDetail.Count + 1, 2);
                        doctable.Design = TableDesign.TableGrid;
                        doctable.TableCaption = "Список клиентов";

                        doctable.Rows[0].Cells[0].Paragraphs[0].Append("Список клиентов").Font("Times New Roman").FontSize(14);

                        for (int i = 0; i < modelClientDetail.Count; i++)
                        {
                            doctable.Rows[i + 1].Cells[0].Paragraphs[0].Append(modelClientDetail[i].ClientName).Font("Times New Roman").FontSize(14);
                        }
                        document.InsertParagraph().InsertTableAfterSelf(doctable);
                        document.Save();
                        MessageBox.Show("Отчет успешно сформирован!");
                        Process.Start(pathDocumentDOCX);
                        break;

                    case (".xlsx"):
                        Excel.Application excel;
                        Excel.Workbook worKbooK;
                        Excel.Worksheet worKsheeT;
                        Excel.Range celLrangE;

                        string pathDocumentXLSX = Session.baseDir + "Список клиентов" + extension;

                        try
                        {
                            excel = new Excel.Application();
                            excel.Visible = false;
                            excel.DisplayAlerts = false;
                            worKbooK = excel.Workbooks.Add(Type.Missing);


                            worKsheeT = (Microsoft.Office.Interop.Excel.Worksheet)worKbooK.ActiveSheet;
                            worKsheeT.Name = "Список клиентов";

                            worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[1, 8]].Merge();
                            worKsheeT.Cells[1, 1] = "Список клиентов";
                            worKsheeT.Cells.Font.Size = 15;

                            for (int i = 0; i < modelClientDetail.Count; i++)
                            {
                                worKsheeT.Cells[i + 3, 1] = modelClientDetail[i].ClientName;
                            }

                            worKbooK.SaveAs(pathDocumentXLSX); ;
                            worKbooK.Close();
                            excel.Quit();
                            MessageBox.Show("Отчет успешно сформирован!");
                            Process.Start(pathDocumentXLSX);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);

                        }
                        finally
                        {
                            worKsheeT = null;
                            celLrangE = null;
                            worKbooK = null;
                        }

                        break;


                    case (".pdf"):
                        string pathDocumentPDF = Session.baseDir + "Список клиентов" + extension;
                        if (File.Exists(Session.baseDir + "Список клиентов.docx"))
                        {
                            Word.Application appWord = new Word.Application();
                            var wordDocument = appWord.Documents.Open(Session.baseDir + "Список клиентов.docx");
                            wordDocument.ExportAsFixedFormat(pathDocumentPDF, Word.WdExportFormat.wdExportFormatPDF);
                            MessageBox.Show("Отчет успешно сформирован!");
                            wordDocument.Close();
                            Process.Start(pathDocumentPDF);
                        }
                        else
                            MessageBox.Show("Сначала сформируйте отчет .docx");
                        break;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Отсутсвие Ms Office на компьютере. Пожалуйста скачайте его.");
                Process.Start("https://www.microsoft.com/ru-ru/microsoft-365/compare-all-microsoft-365-products?tab=1&rtc=1");
            }
        }

        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(new ThreadStart(createExportDoc));
            t.Start();
        }

        private void ComboBoxExport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem typeItem = (ComboBoxItem)comboBoxExport.SelectedItem;
            extension = typeItem.Content.ToString();



        }
    }
}
