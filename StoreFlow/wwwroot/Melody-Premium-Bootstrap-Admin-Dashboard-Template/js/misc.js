(function ($) {
    'use strict';

    $(function () {
        var body = $('body');
        var contentWrapper = $('.content-wrapper');
        var scroller = $('.container-scroller');
        var footer = $('.footer');
        var sidebar = $('.sidebar');

        // URL'ye göre aktif menüyü belirler
        $('.nav-item', sidebar).removeClass('active');
        $('.nav-link', sidebar).removeClass('active');
        $('.collapse', sidebar).removeClass('show');

        var currentPath = window.location.pathname
            .replace(/\/+$/, '')
            .toLowerCase();

        $('.nav li a', sidebar).each(function () {
            var $this = $(this);
            var href = $this.attr('href');

            // Collapse menü baðlantýlarýný ve boþ linkleri atla
            if (
                !href ||
                href.startsWith('#') ||
                href === 'javascript:void(0)'
            ) {
                return;
            }

            var linkPath = new URL(href, window.location.origin)
                .pathname
                .replace(/\/+$/, '')
                .toLowerCase();

            if (linkPath === currentPath) {
                $this.addClass('active');
                $this.closest('.nav-item').addClass('active');

                var $collapseMenu = $this.closest('.collapse');

                if ($collapseMenu.length) {
                    $collapseMenu.addClass('show');

                    $collapseMenu
                        .prev('.nav-link')
                        .attr('aria-expanded', 'true');

                    $collapseMenu
                        .closest('.nav-item')
                        .addClass('active');
                }
            }
        });

        // Bir alt menü açýldýðýnda diðerlerini kapatýr
        sidebar.on('show.bs.collapse', '.collapse', function () {
            sidebar.find('.collapse.show').not(this).collapse('hide');
        });

        // Sidebar ve içerik yüksekliði
        applyStyles();

        function applyStyles() {
            // Perfect Scrollbar
            if (!body.hasClass('rtl')) {
                if (
                    $('.settings-panel .tab-content .tab-pane.scroll-wrapper').length
                ) {
                    const settingsPanelScroll = new PerfectScrollbar(
                        '.settings-panel .tab-content .tab-pane.scroll-wrapper'
                    );
                }

                if ($('.chats').length) {
                    const chatsScroll = new PerfectScrollbar('.chats');
                }

                if (body.hasClass('sidebar-fixed')) {
                    var fixedSidebarScroll = new PerfectScrollbar('#sidebar .nav');
                }
            }
        }

        $('[data-toggle="minimize"]').on('click', function () {
            if (
                body.hasClass('sidebar-toggle-display') ||
                body.hasClass('sidebar-absolute')
            ) {
                body.toggleClass('sidebar-hidden');
            } else {
                body.toggleClass('sidebar-icon-only');
            }
        });

        // Checkbox ve radio butonlarý
        $('.form-check label, .form-radio label').append(
            '<i class="input-helper"></i>'
        );

        // Tam ekran
        $('#fullscreen-button').on('click', function toggleFullScreen() {
            if (
                (document.fullScreenElement !== undefined &&
                    document.fullScreenElement === null) ||
                (document.msFullscreenElement !== undefined &&
                    document.msFullscreenElement === null) ||
                (document.mozFullScreen !== undefined &&
                    !document.mozFullScreen) ||
                (document.webkitIsFullScreen !== undefined &&
                    !document.webkitIsFullScreen)
            ) {
                if (document.documentElement.requestFullScreen) {
                    document.documentElement.requestFullScreen();
                } else if (document.documentElement.mozRequestFullScreen) {
                    document.documentElement.mozRequestFullScreen();
                } else if (document.documentElement.webkitRequestFullScreen) {
                    document.documentElement.webkitRequestFullScreen(
                        Element.ALLOW_KEYBOARD_INPUT
                    );
                } else if (document.documentElement.msRequestFullscreen) {
                    document.documentElement.msRequestFullscreen();
                }
            } else {
                if (document.cancelFullScreen) {
                    document.cancelFullScreen();
                } else if (document.mozCancelFullScreen) {
                    document.mozCancelFullScreen();
                } else if (document.webkitCancelFullScreen) {
                    document.webkitCancelFullScreen();
                } else if (document.msExitFullscreen) {
                    document.msExitFullscreen();
                }
            }
        });
    });
})(jQuery);