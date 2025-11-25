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
            // 실제로는 DI Container를 사용해야 하지만
            // 연습용이므로 직접 생성
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
        /// ViewModel 형태로 반환하여 동적 렌더링 가능
        /// </summary>
        [HttpPost]
        public JsonResult GetPreviewData()
        {
            try
            {
                // Service를 통해 ViewModel 조회
                var viewModel = _exportService.GetPreviewData();

                return Json(new
                {
                    success = true,
                    data = viewModel
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

        /// <summary>
        /// POST: DataExport/DownloadJson
        /// JSON 파일 다운로드용 데이터 조회
        /// 원본 Model 형태로 반환
        /// </summary>
        [HttpPost]
        public JsonResult DownloadJson()
        {
            try
            {
                // 원본 데이터 조회
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