using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrameworkApp.Models
{
    /// <summary>
    /// 데이터 미리보기용 ViewModel
    /// </summary>
    public class DataPreviewViewModel
    {
        /// <summary>
        /// 기본 정보 (키-값 쌍)
        /// </summary>
        public Dictionary<string, string> BasicInfo { get; set; }

        /// <summary>
        /// 섹션 목록 (동적 탭/테이블)
        /// </summary>
        public List<DataSection> Sections { get; set; }
    }

    /// <summary>
    /// 데이터 섹션 (탭 하나에 해당)
    /// </summary>
    public class DataSection
    {
        /// <summary>
        /// 섹션 ID (탭 식별자)
        /// </summary>
        public string SectionId { get; set; }

        /// <summary>
        /// 섹션 이름 (탭에 표시될 이름)
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        /// 섹션 타입 (table, cards, keyvalue 등)
        /// </summary>
        public SectionType Type { get; set; }

        /// <summary>
        /// 컬럼 정의 (테이블일 경우)
        /// </summary>
        public List<ColumnDefinition> Columns { get; set; }

        /// <summary>
        /// 데이터 행 목록
        /// </summary>
        public List<Dictionary<string, object>> Rows { get; set; }
    }

    /// <summary>
    /// 컬럼 정의
    /// </summary>
    public class ColumnDefinition
    {
        /// <summary>
        /// 컬럼 키 (데이터의 프로퍼티명)
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 컬럼 표시 이름
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 컬럼 타입
        /// </summary>
        public ColumnType Type { get; set; }

        /// <summary>
        /// 정렬 방향 (left, center, right)
        /// </summary>
        public string Align { get; set; }

        /// <summary>
        /// 포맷 (예: currency, date, percentage)
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 조건부 스타일링 규칙
        /// </summary>
        public List<StyleRule> StyleRules { get; set; }
    }

    /// <summary>
    /// 스타일 규칙 (조건부 CSS 적용)
    /// </summary>
    public class StyleRule
    {
        public string Condition { get; set; }  // 예: "equals", "greater", "less"
        public object Value { get; set; }
        public string CssClass { get; set; }
    }

    /// <summary>
    /// 섹션 타입
    /// </summary>
    public enum SectionType
    {
        Table,      // 테이블 형태
        Cards,      // 카드 형태
        KeyValue    // 키-값 리스트
    }

    /// <summary>
    /// 컬럼 타입
    /// </summary>
    public enum ColumnType
    {
        Text,       // 일반 텍스트
        Number,     // 숫자
        Currency,   // 통화
        Date,       // 날짜
        Status,     // 상태 (Badge)
        Boolean     // 참/거짓
    }
}