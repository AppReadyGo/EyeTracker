
--======================= Staff roles =============================--

INSERT INTO [usr].[StaffRoles](ID, Name)
VALUES(1, 'Administrator')


--======================= Init database ===========================--
INSERT INTO [usr].[Memberships](ID, Name)
VALUES  (1, 'Basic'),
		(2, 'Plus'),
		(3, 'Pro')