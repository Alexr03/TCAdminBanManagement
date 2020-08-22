DROP TABLE tcmodule_banned_ips;

DELETE FROM tc_modules WHERE module_id LIKE '918250d5-e6d5-4398-9434-44b0a17cd231';
DELETE FROM tc_site_map WHERE module_id LIKE '918250d5-e6d5-4398-9434-44b0a17cd231';
DELETE FROM tc_permission_categories WHERE module_id LIKE '918250d5-e6d5-4398-9434-44b0a17cd231';
DELETE FROM tc_permissions WHERE module_id LIKE '918250d5-e6d5-4398-9434-44b0a17cd231';
DELETE FROM tc_panelbar_categories WHERE module_id LIKE '918250d5-e6d5-4398-9434-44b0a17cd231';