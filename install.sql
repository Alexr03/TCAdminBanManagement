create table tcmodule_banned_ips
(
    id        int      not null
        primary key,
    ipAddress text     not null,
    reason    text     null,
    banType   int      not null,
    expiresAt datetime null,
    app_data  text     null
);

# ----------------------------------------------------------------------------------------------------------------------

INSERT INTO tc_modules (module_id, display_name, version, enabled, config_page, component_directory,
                        security_class)
VALUES ('918250d5-e6d5-4398-9434-44b0a17cd231', 'Ban Management', '2.0', 1, null, null, null);

# ----------------------------------------------------------------------------------------------------------------------

INSERT INTO tc_site_map (page_id, module_id, parent_page_id, parent_page_module_id, category_id, url, mvc_url,
                         controller, action, display_name, page_small_icon, panelbar_icon, show_in_sidebar,
                         view_order, required_permissions, menu_required_permissions, page_manager,
                         page_search_provider, cache_name)
VALUES (1, '918250d5-e6d5-4398-9434-44b0a17cd231', 40, '07405876-e8c2-4b24-a774-4ef57f596384', 1, '/BanManagement',
        '/BanManagement', 'BanManagement', 'Index', 'Manage Bans', 'MenuIcons/Base/ServerComponents24x24.png',
        'MenuIcons/Base/ServerComponents16x16.png', 1, 1000, '({918250d5-e6d5-4398-9434-44b0a17cd231,1,16})
({07405876-e8c2-4b24-a774-4ef57f596384,0,0})
', '({918250d5-e6d5-4398-9434-44b0a17cd231,1,16})
({07405876-e8c2-4b24-a774-4ef57f596384,0,0})
', null, null, null);

INSERT INTO tc_site_map (page_id, module_id, parent_page_id, parent_page_module_id, category_id, url, mvc_url,
                         controller, action, display_name, page_small_icon, panelbar_icon, show_in_sidebar,
                         view_order, required_permissions, menu_required_permissions, page_manager,
                         page_search_provider, cache_name)
VALUES (2, '918250d5-e6d5-4398-9434-44b0a17cd231', null, null, null, '/BanManagement/Banned', '/BanManagement/Banned',
        'BanManagement', 'Banned', 'Banned', 'MenuIcons/Base/ServerComponents24x24.png',
        'MenuIcons/Base/ServerComponents24x24.png', 0, 1000, null, null, null, null, null);

INSERT INTO tc_site_map (page_id, module_id, parent_page_id, parent_page_module_id, category_id, url, mvc_url,
                         controller, action, display_name, page_small_icon, panelbar_icon, show_in_sidebar,
                         view_order, required_permissions, menu_required_permissions, page_manager,
                         page_search_provider, cache_name)
VALUES (3, '918250d5-e6d5-4398-9434-44b0a17cd231', 40, '07405876-e8c2-4b24-a774-4ef57f596384', 1,
        '/BanManagement/AutomatedBans', '/BanManagement/AutomatedBans', 'BanManagement', 'AutomatedBans',
        'Manage Automatic Bans', 'MenuIcons/Base/ServerComponents24x24.png', 'MenuIcons/Base/ServerComponents16x16.png',
        1, 1000, '({918250d5-e6d5-4398-9434-44b0a17cd231,4,16})
({07405876-e8c2-4b24-a774-4ef57f596384,0,0})
', '({918250d5-e6d5-4398-9434-44b0a17cd231,4,16})
({07405876-e8c2-4b24-a774-4ef57f596384,0,0})
', null, null, null);

# ----------------------------------------------------------------------------------------------------------------------

INSERT INTO tc_permission_categories (category_id, module_id, parent_category_id, parent_module_id,
                                      display_name, view_order)
VALUES (1, '918250d5-e6d5-4398-9434-44b0a17cd231', null, null, 'Ban Management', 1050);

# ----------------------------------------------------------------------------------------------------------------------

INSERT INTO tc_permissions (permission_id, module_id, category_id, display_name, permission_type, view_order,
                            role_owner_required_permissions, same_role_required_permissions, top_level_only)
VALUES (1, '918250d5-e6d5-4398-9434-44b0a17cd231', 1, 'View Bans', 1, 1000, '', null, 1);
INSERT INTO tc_permissions (permission_id, module_id, category_id, display_name, permission_type, view_order,
                            role_owner_required_permissions, same_role_required_permissions, top_level_only)
VALUES (2, '918250d5-e6d5-4398-9434-44b0a17cd231', 1, 'Add Ban', 1, 1000, '', null, 1);
INSERT INTO tc_permissions (permission_id, module_id, category_id, display_name, permission_type, view_order,
                            role_owner_required_permissions, same_role_required_permissions, top_level_only)
VALUES (3, '918250d5-e6d5-4398-9434-44b0a17cd231', 1, 'Remove Ban', 1, 1000, '', null, 1);
INSERT INTO tc_permissions (permission_id, module_id, category_id, display_name, permission_type, view_order,
                            role_owner_required_permissions, same_role_required_permissions, top_level_only)
VALUES (4, '918250d5-e6d5-4398-9434-44b0a17cd231', 1, 'Manage Automatic Bans', 1, 1000, '', null, 1);

# ----------------------------------------------------------------------------------------------------------------------

INSERT INTO tc_panelbar_categories (category_id, module_id, display_name, view_order, parent_category_id,
                                    parent_module_id, page_id, panelbar_icon)
VALUES (1, '918250d5-e6d5-4398-9434-44b0a17cd231', 'Ban Management', 1000, 6, '07405876-e8c2-4b24-a774-4ef57f596384',
        null, null);

# ----------------------------------------------------------------------------------------------------------------------

INSERT INTO tc_module_commands (command_id, module_id, is_custom, description, sender_class, command_name,
                                command_class, execute_order, enabled, can_disable, master_only)
VALUES (1, '918250d5-e6d5-4398-9434-44b0a17cd231', 1, 'Deny access to Panel Login if banned',
        'TCAdmin.SDK.Objects.User', 'PanelLogin', 'TCAdminBanManagement.Events.LoginProtection, TCAdminBanManagement',
        100, 1, 0, null);
INSERT INTO tc_module_commands (command_id, module_id, is_custom, description, sender_class, command_name,
                                command_class, execute_order, enabled, can_disable, master_only)
VALUES (3, '918250d5-e6d5-4398-9434-44b0a17cd231', 1, 'When a banned user attempts to access the panel or ftp',
        'TCAdminBanManagement.Models.Objects.BannedIp', 'AttemptedAccess',
        'TCAdminBanManagement.Events.BannedIpEvent, TCAdminBanManagement', 100, 1, 0, null);
INSERT INTO tc_module_commands (command_id, module_id, is_custom, description, sender_class, command_name,
                                command_class, execute_order, enabled, can_disable, master_only)
VALUES (2, '918250d5-e6d5-4398-9434-44b0a17cd231', 1, 'Deny access to FTP if banned', 'TCAdmin.SDK.Objects.User',
        'FtpLogin', 'TCAdminBanManagement.Events.LoginProtection, TCAdminBanManagement', 100, 1, 0, null);
INSERT INTO tc_module_commands (command_id, module_id, is_custom, description, sender_class, command_name,
                                command_class, execute_order, enabled, can_disable, master_only)
VALUES (4, '918250d5-e6d5-4398-9434-44b0a17cd231', 1, 'Failed Login Attempts Automatic Banning',
        'TCAdminBanManagement.Models.Objects.BannedIp', 'FailedLogin',
        'TCAdminBanManagement.Events.BannedIpEvent, TCAdminBanManagement', 100, 1, 0, null);