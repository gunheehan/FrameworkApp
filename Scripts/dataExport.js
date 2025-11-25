/**
 * 데이터 내보내기 JavaScript 모듈
 * 동적 렌더링 및 확장 가능한 구조
 */
var DataExportModule = (function ($) {
    'use strict';

    // 모듈 전역 변수
    var currentViewModel = null;
    var originalData = null;

    // 렌더러 매핑
    var renderers = {
        'Table': renderTableSection,
        'Cards': renderCardsSection,
        'KeyValue': renderKeyValueSection
    };

    /**
     * 모듈 초기화
     */
    function init() {
        bindEvents();
    }

    /**
     * 이벤트 바인딩
     */
    function bindEvents() {
        $('#btnShowPreview').on('click', handlePreviewClick);
        $('#btnDownload').on('click', handleDownloadClick);
    }

    /**
     * 미리보기 버튼 클릭
     */
    function handlePreviewClick() {
        var $btn = $(this);
        setButtonLoading($btn, true);

        $.ajax({
            url: '/DataExport/GetPreviewData',
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    currentViewModel = response.data;
                    renderPreview(currentViewModel);
                    $('#previewModal').modal('show');
                } else {
                    showError('데이터 로딩 실패: ' + response.message);
                }
            },
            error: function (xhr, status, error) {
                showError('서버 오류: ' + error);
            },
            complete: function () {
                setButtonLoading($btn, false);
            }
        });
    }

    /**
     * 다운로드 버튼 클릭
     */
    function handleDownloadClick() {
        $.ajax({
            url: '/DataExport/DownloadJson',
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    downloadJsonFile(response.data);
                    showSuccess('파일이 다운로드되었습니다.');
                    $('#previewModal').modal('hide');
                } else {
                    showError('다운로드 실패: ' + response.message);
                }
            },
            error: function (xhr, status, error) {
                showError('서버 오류: ' + error);
            }
        });
    }

    /**
     * 미리보기 렌더링 (동적)
     */
    function renderPreview(viewModel) {
        // 기본 정보 렌더링
        renderBasicInfo(viewModel.BasicInfo);

        // 섹션 렌더링
        renderSections(viewModel.Sections);
    }

    /**
     * 기본 정보 렌더링
     */
    function renderBasicInfo(basicInfo) {
        var html = '';
        for (var key in basicInfo) {
            if (basicInfo.hasOwnProperty(key)) {
                html += '<strong>' + key + ':</strong> ' + basicInfo[key] + '<br/>';
            }
        }
        $('#basicInfoContainer').html(html);
    }

    /**
     * 섹션 렌더링 (탭 + 컨텐츠)
     */
    function renderSections(sections) {
        var tabsHtml = '';
        var contentHtml = '';

        $.each(sections, function (index, section) {
            var isActive = index === 0 ? 'active' : '';

            // 탭 생성
            tabsHtml += '<li role="presentation" class="' + isActive + '">' +
                '<a href="#' + section.SectionId + '" role="tab" data-toggle="tab">' +
                section.SectionName +
                '</a></li>';

            // 컨텐츠 생성
            var renderer = renderers[section.Type];
            var sectionContent = renderer ? renderer(section) : '<p>지원하지 않는 섹션 타입입니다.</p>';

            contentHtml += '<div role="tabpanel" class="tab-pane ' + isActive + '" id="' + section.SectionId + '">' +
                sectionContent +
                '</div>';
        });

        $('#dynamicTabs').html(tabsHtml);
        $('#dynamicTabContent').html(contentHtml);
    }

    /**
     * 테이블 섹션 렌더링
     */
    function renderTableSection(section) {
        var html = '<div class="table-responsive">' +
            '<table class="table table-striped table-bordered table-hover">' +
            '<thead><tr class="info">';

        // 헤더 생성
        $.each(section.Columns, function (i, col) {
            html += '<th class="text-' + col.Align + '">' + col.DisplayName + '</th>';
        });

        html += '</tr></thead><tbody>';

        // 데이터 행 생성
        $.each(section.Rows, function (i, row) {
            html += '<tr>';
            $.each(section.Columns, function (j, col) {
                var value = row[col.Key];
                var formattedValue = formatValue(value, col);
                var cellClass = getCellClass(value, col);

                html += '<td class="text-' + col.Align + ' ' + cellClass + '">' +
                    formattedValue +
                    '</td>';
            });
            html += '</tr>';
        });

        html += '</tbody></table></div>';
        return html;
    }

    /**
     * 카드 섹션 렌더링 (요약 정보)
     */
    function renderCardsSection(section) {
        var html = '<div class="row">';

        $.each(section.Rows, function (i, card) {
            var panelType = card.type || 'default';
            var value = card.value;

            // 포맷 적용
            if (card.format === 'currency') {
                value = '₩' + Number(value).toLocaleString();
            }

            html += '<div class="col-md-3 col-sm-6">' +
                '<div class="panel panel-' + panelType + '">' +
                '<div class="panel-heading text-center">' +
                '<h4>' + card.label + '</h4>' +
                '</div>' +
                '<div class="panel-body text-center">' +
                '<h2>' + value + '</h2>' +
                '</div>' +
                '</div>' +
                '</div>';
        });

        html += '</div>';
        return html;
    }

    /**
     * 키-값 섹션 렌더링
     */
    function renderKeyValueSection(section) {
        var html = '<dl class="dl-horizontal">';

        $.each(section.Rows, function (i, row) {
            for (var key in row) {
                if (row.hasOwnProperty(key)) {
                    html += '<dt>' + key + '</dt>';
                    html += '<dd>' + row[key] + '</dd>';
                }
            }
        });

        html += '</dl>';
        return html;
    }

    /**
     * 값 포맷팅
     */
    function formatValue(value, column) {
        if (value === null || value === undefined) {
            return '-';
        }

        switch (column.Type) {
            case 'Currency':
                return '₩' + Number(value).toLocaleString();
            case 'Number':
                return Number(value).toLocaleString();
            case 'Status':
                var cssClass = getStyleClass(value, column);
                return '<span class="label label-' + cssClass + '">' + value + '</span>';
            default:
                return value;
        }
    }

    /**
     * 셀 CSS 클래스 가져오기
     */
    function getCellClass(value, column) {
        // 추가 스타일링 필요시 구현
        return '';
    }

    /**
     * 스타일 규칙에 따른 CSS 클래스 가져오기
     */
    function getStyleClass(value, column) {
        if (!column.StyleRules || column.StyleRules.length === 0) {
            return 'default';
        }

        for (var i = 0; i < column.StyleRules.length; i++) {
            var rule = column.StyleRules[i];
            if (rule.Condition === 'equals' && rule.Value === value) {
                return rule.CssClass;
            }
        }

        return 'default';
    }

    /**
     * JSON 파일 다운로드
     */
    function downloadJsonFile(data) {
        var jsonString = JSON.stringify(data, null, 2);
        var blob = new Blob([jsonString], { type: 'application/json;charset=utf-8;' });
        var link = document.createElement('a');
        var url = URL.createObjectURL(blob);

        var fileName = 'export_' + formatDateTime(new Date()) + '.json';
        link.setAttribute('href', url);
        link.setAttribute('download', fileName);
        link.style.visibility = 'hidden';

        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        URL.revokeObjectURL(url);
    }

    /**
     * 버튼 로딩 상태 설정
     */
    function setButtonLoading($btn, isLoading) {
        if (isLoading) {
            $btn.prop('disabled', true)
                .html('<span class="glyphicon glyphicon-refresh glyphicon-refresh-animate"></span> 로딩중...');
        } else {
            $btn.prop('disabled', false)
                .html('<span class="glyphicon glyphicon-export"></span> 데이터 미리보기');
        }
    }

    /**
     * 에러 메시지 표시
     */
    function showError(message) {
        alert('오류: ' + message);
    }

    /**
     * 성공 메시지 표시
     */
    function showSuccess(message) {
        alert(message);
    }

    /**
     * 날짜 포맷
     */
    function formatDateTime(date) {
        var year = date.getFullYear();
        var month = String(date.getMonth() + 1).padStart(2, '0');
        var day = String(date.getDate()).padStart(2, '0');
        var hours = String(date.getHours()).padStart(2, '0');
        var minutes = String(date.getMinutes()).padStart(2, '0');
        var seconds = String(date.getSeconds()).padStart(2, '0');
        return year + month + day + '_' + hours + minutes + seconds;
    }

    // Public API
    return {
        init: init,
        // 확장을 위한 커스텀 렌더러 등록
        registerRenderer: function (type, renderer) {
            renderers[type] = renderer;
        }
    };

})(jQuery);

// DOM 준비되면 초기화
$(document).ready(function () {
    DataExportModule.init();
});