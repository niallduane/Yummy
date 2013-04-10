using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Reporting.WebForms;
using System.Web;

namespace Yummy.Common.Extensions
{
    public static class ReportViewerExtensions
    {
        /// <summary>
        /// Exports the local report in the Webpage ReportViewer to an Excel Spreadsheet with the specified filename.
        /// </summary>
        /// <param name="viewer"></param>
        /// <param name="filename"></param>
        public static void ExportToExcel(this ReportViewer viewer, String filename)
        {
            HttpContext http = HttpContext.Current;

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            string deviceinfo = "<DeviceInfo><OutputFormat>Excel</OutputFormat></DeviceInfo>";

            byte[] bytes = viewer.LocalReport.Render("Excel", deviceinfo, out mimeType, out encoding, out extension, out streamids, out warnings);

            http.Response.ContentType = "application/Excel";// set the MIME type here
            http.Response.AddHeader("content-disposition", "attachment; filename=" + filename);
            http.Response.BinaryWrite(bytes);
        }

        /// <summary>
        /// Exports the local report in the Webpage ReportViewer to a PDF Document with the specified filename.
        /// </summary>
        /// <param name="viewer"></param>
        /// <param name="filename"></param>
        public static void ExportToPDF(this ReportViewer viewer, String filename)
        {
            HttpContext http = HttpContext.Current;

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            string deviceinfo = "<DeviceInfo><OutputFormat>PDF</OutputFormat></DeviceInfo>";

            byte[] bytes = viewer.LocalReport.Render("PDF", deviceinfo, out mimeType, out encoding, out extension, out streamids, out warnings);

            http.Response.ContentType = "application/pdf";// set the MIME type here
            http.Response.AddHeader("content-disposition", "attachment; filename=" + filename);
            http.Response.BinaryWrite(bytes);
        }
    }
}
