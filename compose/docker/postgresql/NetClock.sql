--
-- PostgreSQL database dump
--

-- Dumped from database version 11.7
-- Dumped by pg_dump version 11.7

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: AspNetRoleClaims; Type: TABLE; Schema: public; Owner: netclock
--

CREATE TABLE public."AspNetRoleClaims" (
    "Id" integer NOT NULL,
    "RoleId" text NOT NULL,
    "ClaimType" text,
    "ClaimValue" text
);


ALTER TABLE public."AspNetRoleClaims" OWNER TO netclock;

--
-- Name: AspNetRoleClaims_Id_seq; Type: SEQUENCE; Schema: public; Owner: netclock
--

ALTER TABLE public."AspNetRoleClaims" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."AspNetRoleClaims_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: AspNetRoles; Type: TABLE; Schema: public; Owner: netclock
--

CREATE TABLE public."AspNetRoles" (
    "Id" text NOT NULL,
    "Name" character varying(256),
    "NormalizedName" character varying(256),
    "ConcurrencyStamp" text
);


ALTER TABLE public."AspNetRoles" OWNER TO netclock;

--
-- Name: AspNetUserClaims; Type: TABLE; Schema: public; Owner: netclock
--

CREATE TABLE public."AspNetUserClaims" (
    "Id" integer NOT NULL,
    "UserId" text NOT NULL,
    "ClaimType" text,
    "ClaimValue" text
);


ALTER TABLE public."AspNetUserClaims" OWNER TO netclock;

--
-- Name: AspNetUserClaims_Id_seq; Type: SEQUENCE; Schema: public; Owner: netclock
--

ALTER TABLE public."AspNetUserClaims" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."AspNetUserClaims_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: AspNetUserLogins; Type: TABLE; Schema: public; Owner: netclock
--

CREATE TABLE public."AspNetUserLogins" (
    "LoginProvider" character varying(128) NOT NULL,
    "ProviderKey" character varying(128) NOT NULL,
    "ProviderDisplayName" text,
    "UserId" text NOT NULL
);


ALTER TABLE public."AspNetUserLogins" OWNER TO netclock;

--
-- Name: AspNetUserRoles; Type: TABLE; Schema: public; Owner: netclock
--

CREATE TABLE public."AspNetUserRoles" (
    "UserId" text NOT NULL,
    "RoleId" text NOT NULL
);


ALTER TABLE public."AspNetUserRoles" OWNER TO netclock;

--
-- Name: AspNetUserTokens; Type: TABLE; Schema: public; Owner: netclock
--

CREATE TABLE public."AspNetUserTokens" (
    "UserId" text NOT NULL,
    "LoginProvider" character varying(128) NOT NULL,
    "Name" character varying(128) NOT NULL,
    "Value" text
);


ALTER TABLE public."AspNetUserTokens" OWNER TO netclock;

--
-- Name: AspNetUsers; Type: TABLE; Schema: public; Owner: netclock
--

CREATE TABLE public."AspNetUsers" (
    "Id" text NOT NULL,
    "UserName" character varying(256),
    "NormalizedUserName" character varying(256),
    "Email" character varying(256),
    "NormalizedEmail" character varying(256),
    "EmailConfirmed" boolean NOT NULL,
    "PasswordHash" text,
    "SecurityStamp" text,
    "ConcurrencyStamp" text,
    "PhoneNumber" text,
    "PhoneNumberConfirmed" boolean NOT NULL,
    "TwoFactorEnabled" boolean NOT NULL,
    "LockoutEnd" timestamp with time zone,
    "LockoutEnabled" boolean NOT NULL,
    "AccessFailedCount" integer NOT NULL,
    "Slug" character varying(256) NOT NULL,
    "FirstName" character varying(50) NOT NULL,
    "LastName" character varying(50) NOT NULL,
    "Active" boolean NOT NULL,
    "Created" timestamp without time zone NOT NULL,
    "LastModified" timestamp without time zone NOT NULL
);


ALTER TABLE public."AspNetUsers" OWNER TO netclock;

--
-- Name: DeviceCodes; Type: TABLE; Schema: public; Owner: netclock
--

CREATE TABLE public."DeviceCodes" (
    "UserCode" character varying(200) NOT NULL,
    "DeviceCode" character varying(200) NOT NULL,
    "SubjectId" character varying(200),
    "ClientId" character varying(200) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "Expiration" timestamp without time zone NOT NULL,
    "Data" character varying(50000) NOT NULL
);


ALTER TABLE public."DeviceCodes" OWNER TO netclock;

--
-- Name: PersistedGrants; Type: TABLE; Schema: public; Owner: netclock
--

CREATE TABLE public."PersistedGrants" (
    "Key" character varying(200) NOT NULL,
    "Type" character varying(50) NOT NULL,
    "SubjectId" character varying(200),
    "ClientId" character varying(200) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "Expiration" timestamp without time zone,
    "Data" character varying(50000) NOT NULL
);


ALTER TABLE public."PersistedGrants" OWNER TO netclock;

--
-- Name: Schedules; Type: TABLE; Schema: public; Owner: netclock
--

CREATE TABLE public."Schedules" (
    "Id" bigint NOT NULL,
    "CreatedBy" text,
    "Created" timestamp without time zone NOT NULL,
    "LastModifiedBy" text,
    "LastModified" timestamp without time zone,
    "TimeStart" timestamp without time zone NOT NULL,
    "TimeFinish" timestamp without time zone NOT NULL,
    "TimeTotal" interval NOT NULL,
    "Observations" text
);


ALTER TABLE public."Schedules" OWNER TO netclock;

--
-- Name: Schedules_Id_seq; Type: SEQUENCE; Schema: public; Owner: netclock
--

ALTER TABLE public."Schedules" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Schedules_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: netclock
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO netclock;

--
-- Data for Name: AspNetRoleClaims; Type: TABLE DATA; Schema: public; Owner: netclock
--

COPY public."AspNetRoleClaims" ("Id", "RoleId", "ClaimType", "ClaimValue") FROM stdin;
1	c2d3ae7d-8aad-434c-8f9a-34692cf44716	permission	Permissions.Accounts.View
2	c2d3ae7d-8aad-434c-8f9a-34692cf44716	permission	Permissions.Accounts.Create
3	c2d3ae7d-8aad-434c-8f9a-34692cf44716	permission	Permissions.Accounts.Update
4	c2d3ae7d-8aad-434c-8f9a-34692cf44716	permission	Permissions.Accounts.Delete
5	c2d3ae7d-8aad-434c-8f9a-34692cf44716	permission	Permissions.AdminAccounts.View
6	c2d3ae7d-8aad-434c-8f9a-34692cf44716	permission	Permissions.AdminAccounts.Create
7	c2d3ae7d-8aad-434c-8f9a-34692cf44716	permission	Permissions.AdminAccounts.Update
8	c2d3ae7d-8aad-434c-8f9a-34692cf44716	permission	Permissions.AdminAccounts.Delete
9	2549659f-3c40-4ad5-852a-3fdb1a36cf7b	permission	Permissions.Accounts.View
10	2549659f-3c40-4ad5-852a-3fdb1a36cf7b	permission	Permissions.Accounts.Create
11	2549659f-3c40-4ad5-852a-3fdb1a36cf7b	permission	Permissions.Accounts.Update
12	2549659f-3c40-4ad5-852a-3fdb1a36cf7b	permission	Permissions.Accounts.Delete
13	2549659f-3c40-4ad5-852a-3fdb1a36cf7b	permission	Permissions.AdminAccounts.View
14	2549659f-3c40-4ad5-852a-3fdb1a36cf7b	permission	Permissions.AdminAccounts.Create
15	2549659f-3c40-4ad5-852a-3fdb1a36cf7b	permission	Permissions.AdminAccounts.Update
16	2549659f-3c40-4ad5-852a-3fdb1a36cf7b	permission	Permissions.AdminAccounts.Delete
17	d23195be-31af-487f-8993-a76b762940d9	permission	Permissions.Accounts.View
18	d23195be-31af-487f-8993-a76b762940d9	permission	Permissions.Accounts.Update
\.


--
-- Data for Name: AspNetRoles; Type: TABLE DATA; Schema: public; Owner: netclock
--

COPY public."AspNetRoles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp") FROM stdin;
c2d3ae7d-8aad-434c-8f9a-34692cf44716	Superuser	SUPERUSER	a2c29de7-0a75-4bda-aa92-cd7c5beba110
2549659f-3c40-4ad5-852a-3fdb1a36cf7b	Staff	STAFF	2709af51-7fa4-4286-8723-5f6477f6fee0
d23195be-31af-487f-8993-a76b762940d9	Employee	EMPLOYEE	0abc8ff0-510b-47d0-ad3d-7c2e29db497e
\.


--
-- Data for Name: AspNetUserClaims; Type: TABLE DATA; Schema: public; Owner: netclock
--

COPY public."AspNetUserClaims" ("Id", "UserId", "ClaimType", "ClaimValue") FROM stdin;
\.


--
-- Data for Name: AspNetUserLogins; Type: TABLE DATA; Schema: public; Owner: netclock
--

COPY public."AspNetUserLogins" ("LoginProvider", "ProviderKey", "ProviderDisplayName", "UserId") FROM stdin;
\.


--
-- Data for Name: AspNetUserRoles; Type: TABLE DATA; Schema: public; Owner: netclock
--

COPY public."AspNetUserRoles" ("UserId", "RoleId") FROM stdin;
929b430e-688a-4844-9b70-eb84aee00e2b	d23195be-31af-487f-8993-a76b762940d9
929b430e-688a-4844-9b70-eb84aee00e2b	2549659f-3c40-4ad5-852a-3fdb1a36cf7b
929b430e-688a-4844-9b70-eb84aee00e2b	c2d3ae7d-8aad-434c-8f9a-34692cf44716
d66ce9e3-88c7-4f3d-8b1d-57ff025d734b	d23195be-31af-487f-8993-a76b762940d9
d66ce9e3-88c7-4f3d-8b1d-57ff025d734b	2549659f-3c40-4ad5-852a-3fdb1a36cf7b
c991e7a0-2a16-483a-990b-f7fe4225eba8	d23195be-31af-487f-8993-a76b762940d9
f5a10fa5-7c8e-4bc3-a872-3e8b3038d069	d23195be-31af-487f-8993-a76b762940d9
ff0d3d8f-61b9-4408-87d8-b48224effca0	d23195be-31af-487f-8993-a76b762940d9
79103f0b-9d25-45d5-b9a8-69b771dd9c5f	d23195be-31af-487f-8993-a76b762940d9
7440f049-d3a8-4ccb-8d6f-827493390bd2	d23195be-31af-487f-8993-a76b762940d9
42fdbc7d-42fd-4541-ba94-9a4a153fe730	d23195be-31af-487f-8993-a76b762940d9
4d8622d4-d4d6-4215-947b-6ea457195e86	d23195be-31af-487f-8993-a76b762940d9
a1d9a2b9-889a-4aa3-a62d-3e344bc45e2a	d23195be-31af-487f-8993-a76b762940d9
776f6480-ff8b-4ae1-98ca-0eb17d057824	d23195be-31af-487f-8993-a76b762940d9
\.


--
-- Data for Name: AspNetUserTokens; Type: TABLE DATA; Schema: public; Owner: netclock
--

COPY public."AspNetUserTokens" ("UserId", "LoginProvider", "Name", "Value") FROM stdin;
\.


--
-- Data for Name: AspNetUsers; Type: TABLE DATA; Schema: public; Owner: netclock
--

COPY public."AspNetUsers" ("Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount", "Slug", "FirstName", "LastName", "Active", "Created", "LastModified") FROM stdin;
776f6480-ff8b-4ae1-98ca-0eb17d057824	Lorena	LORENA	lorena@example.com	LORENA@EXAMPLE.COM	t	AQAAAAEAACcQAAAAEPoIhElNY3BfQRxgbkzsPB0B8pqGiiS69zNTRaPbkxzNF0HDpqgG+pMbLNSh8prj4w==	SVTFEBJRQD3A2SUQ2773PGDZ4DQKZ3WE	e5d5e796-48c1-4a03-8cd9-e208010024ee	\N	f	f	\N	t	0	lorena	Lorena	Lorena	t	2020-05-21 00:09:01.282594	2020-05-21 00:09:01.282595
929b430e-688a-4844-9b70-eb84aee00e2b	Admin	ADMIN	admin@example.com	ADMIN@EXAMPLE.COM	t	AQAAAAEAACcQAAAAEOw3p0u/jQ3JpGNR05PCrAx8DaEk8u1Vi+nYb1WojPfxYl7Ri9dKMDj/jVMJOh12PQ==	C73LB4CEXKFCQNKBNM6UALTO4SWW4BLE	22029a24-e5dd-4be5-8687-c993fc0d5846	\N	f	f	\N	t	0	admin	Admin	Admin	t	2020-05-21 00:09:00.914861	2020-05-21 00:09:00.914891
d66ce9e3-88c7-4f3d-8b1d-57ff025d734b	Alice	ALICE	alice@example.com	ALICE@EXAMPLE.COM	t	AQAAAAEAACcQAAAAEGvAXNiQFGpx6JnKbTO/ioWkhhDcU+nNRej+Z0IAPzrnlksBXa+gKkmwA+bKnNVLdg==	PF3ENBN6JMRIPHBLAHWTPOHHOLXZEE73	2fbc5844-26b7-4cf6-940b-32fdb01428f0	\N	f	f	\N	t	0	alice	Alice	Alice	t	2020-05-21 00:09:01.033881	2020-05-21 00:09:01.033886
c991e7a0-2a16-483a-990b-f7fe4225eba8	Bob	BOB	bob@example.com	BOB@EXAMPLE.COM	t	AQAAAAEAACcQAAAAEE5tGpDdFjnLmiZaG6AZZDSPiyOCeRCuHptchHEZL8/d51yinQC6etCAjeDWR9SMhg==	JVCBQOAYH6KK4XO3W2IXROEWFRYPJYPE	e024f7c9-32ef-4dd7-b0d4-070202817c53	\N	f	f	\N	t	0	bob	Bob	Bob	t	2020-05-21 00:09:01.071664	2020-05-21 00:09:01.071666
f5a10fa5-7c8e-4bc3-a872-3e8b3038d069	Joe	JOE	joe@example.com	JOE@EXAMPLE.COM	t	AQAAAAEAACcQAAAAEMUXFN+XrGIvy6zzE49wXbl8ob26z2xviOvHamTldDHK5L2PEihdSETka/vyZpptfQ==	YCFS7U6E6XDJK6KMHVJV37ECK6CQ6EPI	d4311433-edc9-4859-9b81-f7424feee72d	\N	f	f	\N	t	0	joe	Joe	Joe	t	2020-05-21 00:09:01.097573	2020-05-21 00:09:01.097574
ff0d3d8f-61b9-4408-87d8-b48224effca0	Maria	MARIA	maria@example.com	MARIA@EXAMPLE.COM	t	AQAAAAEAACcQAAAAEH12YuQkrKJlLVXeItfzi4NR3vTHV8N7W7dbn2rJ2ZncF0E1Xjw0vN/79vnQrjbHDQ==	XO3KEQIMZWIMDFWNCPKOBPWKWFTU66FN	fd5c6dfc-3b9c-42dc-9a95-53711f2ef473	\N	f	f	\N	t	0	maria	Maria	Maria	t	2020-05-21 00:09:01.118678	2020-05-21 00:09:01.118679
79103f0b-9d25-45d5-b9a8-69b771dd9c5f	Jordi	JORDI	jordi@example.com	JORDI@EXAMPLE.COM	t	AQAAAAEAACcQAAAAEOZyzuf6eI+/rrreo53bMvsVlsmXzc2PjD4+sD7PQ0RCjnSS+EqsMa3eROIodCVTKw==	7MYTPCQ6CNIIGO3TZKKWLG3RFRDKQOCQ	5ccc5aba-98e8-44d2-8f5f-edad27ff0773	\N	f	f	\N	t	0	jordi	Jordi	Jordi	t	2020-05-21 00:09:01.142162	2020-05-21 00:09:01.142164
7440f049-d3a8-4ccb-8d6f-827493390bd2	Sonia	SONIA	sonia@example.com	SONIA@EXAMPLE.COM	t	AQAAAAEAACcQAAAAEKCNK7tth8U3V0zXt1b0sLLWbRponhMHp6eFyg021xv0SoZIGz6kzSQxTGOEECKhPA==	AS7KX3KF5WZZ4CYHB2FHP45VFBVSQ4DA	31455a09-fd57-4a6e-8a46-cdb088f4a38f	\N	f	f	\N	t	0	sonia	Sonia	Sonia	t	2020-05-21 00:09:01.167729	2020-05-21 00:09:01.167731
42fdbc7d-42fd-4541-ba94-9a4a153fe730	Sara	SARA	sara@example.com	SARA@EXAMPLE.COM	t	AQAAAAEAACcQAAAAECR+H5ql6TUzt5BpmmRVbCcGxSgTOgvgDaxUim/wnDx0LFxwZxL9aL/tgqadpiBudQ==	YE2MCAIVCJOSXH5CNOSWW32BYFUA5C4J	bbd44aab-7376-4727-9e65-82488cc263a3	\N	f	f	\N	t	0	sara	Sara	Sara	t	2020-05-21 00:09:01.191756	2020-05-21 00:09:01.191757
4d8622d4-d4d6-4215-947b-6ea457195e86	Perico	PERICO	perico@example.com	PERICO@EXAMPLE.COM	t	AQAAAAEAACcQAAAAENo4B2teomY6y41TJlXUhaXYBAP6kSr9XwS65snz78P/YFTNyzMI+JqLnDN9waHnTA==	DD6QWMFW3WP26MPLFPWUCPAYXMU7UWXK	8aae264c-eae5-4938-b230-3776ec70de2c	\N	f	f	\N	t	0	perico	Perico	Perico	t	2020-05-21 00:09:01.214469	2020-05-21 00:09:01.21447
a1d9a2b9-889a-4aa3-a62d-3e344bc45e2a	Palote	PALOTE	palote@example.com	PALOTE@EXAMPLE.COM	t	AQAAAAEAACcQAAAAEO5tnKWPRhW8RPi6oPoNI8rcSAxlUFqY7b+B9wIs51zY7lU1ops1dzFmMtvI4WFZhA==	ENPZDMADR4J5DLQQNCO5UP5A66CISVNG	ba03b5c3-4316-4c1d-a83e-09108e8beeef	\N	f	f	\N	t	0	palote	Palote	Palote	t	2020-05-21 00:09:01.238081	2020-05-21 00:09:01.238083
\.


--
-- Data for Name: DeviceCodes; Type: TABLE DATA; Schema: public; Owner: netclock
--

COPY public."DeviceCodes" ("UserCode", "DeviceCode", "SubjectId", "ClientId", "CreationTime", "Expiration", "Data") FROM stdin;
\.


--
-- Data for Name: PersistedGrants; Type: TABLE DATA; Schema: public; Owner: netclock
--

COPY public."PersistedGrants" ("Key", "Type", "SubjectId", "ClientId", "CreationTime", "Expiration", "Data") FROM stdin;
\.


--
-- Data for Name: Schedules; Type: TABLE DATA; Schema: public; Owner: netclock
--

COPY public."Schedules" ("Id", "CreatedBy", "Created", "LastModifiedBy", "LastModified", "TimeStart", "TimeFinish", "TimeTotal", "Observations") FROM stdin;
\.


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: netclock
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20200323184831_InitialApplication	3.1.4
\.


--
-- Name: AspNetRoleClaims_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: netclock
--

SELECT pg_catalog.setval('public."AspNetRoleClaims_Id_seq"', 18, true);


--
-- Name: AspNetUserClaims_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: netclock
--

SELECT pg_catalog.setval('public."AspNetUserClaims_Id_seq"', 1, false);


--
-- Name: Schedules_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: netclock
--

SELECT pg_catalog.setval('public."Schedules_Id_seq"', 1, false);


--
-- Name: AspNetRoleClaims PK_AspNetRoleClaims; Type: CONSTRAINT; Schema: public; Owner: netclock
--

ALTER TABLE ONLY public."AspNetRoleClaims"
    ADD CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY ("Id");


--
-- Name: AspNetRoles PK_AspNetRoles; Type: CONSTRAINT; Schema: public; Owner: netclock
--

ALTER TABLE ONLY public."AspNetRoles"
    ADD CONSTRAINT "PK_AspNetRoles" PRIMARY KEY ("Id");


--
-- Name: AspNetUserClaims PK_AspNetUserClaims; Type: CONSTRAINT; Schema: public; Owner: netclock
--

ALTER TABLE ONLY public."AspNetUserClaims"
    ADD CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY ("Id");


--
-- Name: AspNetUserLogins PK_AspNetUserLogins; Type: CONSTRAINT; Schema: public; Owner: netclock
--

ALTER TABLE ONLY public."AspNetUserLogins"
    ADD CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey");


--
-- Name: AspNetUserRoles PK_AspNetUserRoles; Type: CONSTRAINT; Schema: public; Owner: netclock
--

ALTER TABLE ONLY public."AspNetUserRoles"
    ADD CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId");


--
-- Name: AspNetUserTokens PK_AspNetUserTokens; Type: CONSTRAINT; Schema: public; Owner: netclock
--

ALTER TABLE ONLY public."AspNetUserTokens"
    ADD CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name");


--
-- Name: AspNetUsers PK_AspNetUsers; Type: CONSTRAINT; Schema: public; Owner: netclock
--

ALTER TABLE ONLY public."AspNetUsers"
    ADD CONSTRAINT "PK_AspNetUsers" PRIMARY KEY ("Id");


--
-- Name: DeviceCodes PK_DeviceCodes; Type: CONSTRAINT; Schema: public; Owner: netclock
--

ALTER TABLE ONLY public."DeviceCodes"
    ADD CONSTRAINT "PK_DeviceCodes" PRIMARY KEY ("UserCode");


--
-- Name: PersistedGrants PK_PersistedGrants; Type: CONSTRAINT; Schema: public; Owner: netclock
--

ALTER TABLE ONLY public."PersistedGrants"
    ADD CONSTRAINT "PK_PersistedGrants" PRIMARY KEY ("Key");


--
-- Name: Schedules PK_Schedules; Type: CONSTRAINT; Schema: public; Owner: netclock
--

ALTER TABLE ONLY public."Schedules"
    ADD CONSTRAINT "PK_Schedules" PRIMARY KEY ("Id");


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: netclock
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: EmailIndex; Type: INDEX; Schema: public; Owner: netclock
--

CREATE INDEX "EmailIndex" ON public."AspNetUsers" USING btree ("NormalizedEmail");


--
-- Name: IX_AspNetRoleClaims_RoleId; Type: INDEX; Schema: public; Owner: netclock
--

CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON public."AspNetRoleClaims" USING btree ("RoleId");


--
-- Name: IX_AspNetUserClaims_UserId; Type: INDEX; Schema: public; Owner: netclock
--

CREATE INDEX "IX_AspNetUserClaims_UserId" ON public."AspNetUserClaims" USING btree ("UserId");


--
-- Name: IX_AspNetUserLogins_UserId; Type: INDEX; Schema: public; Owner: netclock
--

CREATE INDEX "IX_AspNetUserLogins_UserId" ON public."AspNetUserLogins" USING btree ("UserId");


--
-- Name: IX_AspNetUserRoles_RoleId; Type: INDEX; Schema: public; Owner: netclock
--

CREATE INDEX "IX_AspNetUserRoles_RoleId" ON public."AspNetUserRoles" USING btree ("RoleId");


--
-- Name: IX_AspNetUsers_FirstName_LastName; Type: INDEX; Schema: public; Owner: netclock
--

CREATE UNIQUE INDEX "IX_AspNetUsers_FirstName_LastName" ON public."AspNetUsers" USING btree ("FirstName", "LastName");


--
-- Name: IX_AspNetUsers_Slug; Type: INDEX; Schema: public; Owner: netclock
--

CREATE UNIQUE INDEX "IX_AspNetUsers_Slug" ON public."AspNetUsers" USING btree ("Slug");


--
-- Name: IX_DeviceCodes_DeviceCode; Type: INDEX; Schema: public; Owner: netclock
--

CREATE UNIQUE INDEX "IX_DeviceCodes_DeviceCode" ON public."DeviceCodes" USING btree ("DeviceCode");


--
-- Name: IX_DeviceCodes_Expiration; Type: INDEX; Schema: public; Owner: netclock
--

CREATE INDEX "IX_DeviceCodes_Expiration" ON public."DeviceCodes" USING btree ("Expiration");


--
-- Name: IX_PersistedGrants_Expiration; Type: INDEX; Schema: public; Owner: netclock
--

CREATE INDEX "IX_PersistedGrants_Expiration" ON public."PersistedGrants" USING btree ("Expiration");


--
-- Name: IX_PersistedGrants_SubjectId_ClientId_Type; Type: INDEX; Schema: public; Owner: netclock
--

CREATE INDEX "IX_PersistedGrants_SubjectId_ClientId_Type" ON public."PersistedGrants" USING btree ("SubjectId", "ClientId", "Type");


--
-- Name: RoleNameIndex; Type: INDEX; Schema: public; Owner: netclock
--

CREATE UNIQUE INDEX "RoleNameIndex" ON public."AspNetRoles" USING btree ("NormalizedName");


--
-- Name: UserNameIndex; Type: INDEX; Schema: public; Owner: netclock
--

CREATE UNIQUE INDEX "UserNameIndex" ON public."AspNetUsers" USING btree ("NormalizedUserName");


--
-- Name: AspNetRoleClaims FK_AspNetRoleClaims_AspNetRoles_RoleId; Type: FK CONSTRAINT; Schema: public; Owner: netclock
--

ALTER TABLE ONLY public."AspNetRoleClaims"
    ADD CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES public."AspNetRoles"("Id") ON DELETE CASCADE;


--
-- Name: AspNetUserClaims FK_AspNetUserClaims_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: netclock
--

ALTER TABLE ONLY public."AspNetUserClaims"
    ADD CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AspNetUsers"("Id") ON DELETE CASCADE;


--
-- Name: AspNetUserLogins FK_AspNetUserLogins_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: netclock
--

ALTER TABLE ONLY public."AspNetUserLogins"
    ADD CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AspNetUsers"("Id") ON DELETE CASCADE;


--
-- Name: AspNetUserRoles FK_AspNetUserRoles_AspNetRoles_RoleId; Type: FK CONSTRAINT; Schema: public; Owner: netclock
--

ALTER TABLE ONLY public."AspNetUserRoles"
    ADD CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES public."AspNetRoles"("Id") ON DELETE CASCADE;


--
-- Name: AspNetUserRoles FK_AspNetUserRoles_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: netclock
--

ALTER TABLE ONLY public."AspNetUserRoles"
    ADD CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AspNetUsers"("Id") ON DELETE CASCADE;


--
-- Name: AspNetUserTokens FK_AspNetUserTokens_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: netclock
--

ALTER TABLE ONLY public."AspNetUserTokens"
    ADD CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AspNetUsers"("Id") ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--
