using FrameworkApp.Service;
using System;
using System.Web.Mvc;

namespace FrameworkApp.Controllers
{
    /// <summary>
    /// 데이터 내보내기 컨트롤러
    /// 요청을 받아서 Service를 호출하고 결과를 View에 전달
    /// </summary>
    public class DataExportController : Controller
    {
        private readonly IDataExportService _exportService;

        /// <summary>
        /// 생성자 - Service 주입
        /// </summary>
        public DataExportController()
        {
            _exportService = new DataExportService();
        }

        /// <summary>
        /// GET: DataExport/Index
        /// 메인 페이지 표시
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// POST: DataExport/GetPreviewData
        /// 미리보기 데이터 조회 (Ajax 요청)
        /// </summary>
        [HttpPost]
        public JsonResult GetPreviewData()
        {
            try
            {
                // Service를 통해 데이터 조회
                var data = _exportService.GetExportData();

                return Json(new
                {
                    success = true,
                    data = data
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}