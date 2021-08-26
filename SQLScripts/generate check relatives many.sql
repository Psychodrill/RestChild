declare @nguid as varchar(100)
declare @number as varchar(100)
declare @request as NVARCHAR(MAX)
declare @index as int

set @index = 514210

DECLARE childs  CURSOR   
     FOR select top 1
'<lastname3>' + rtrim(ltrim(isnull(a.LastName, ''))) + '</lastname3>
<firstname3>' + rtrim(ltrim(isnull(a.FirstName, ''))) + '</firstname3>
<middlename>' + rtrim(ltrim(isnull(c.MiddleName, ''))) + '</middlename>
<middlename3>' + rtrim(ltrim(isnull(a.MiddleName, ''))) + '</middlename3>
<birthdate3>' + replace(convert(varchar, a.DateOfBirth,102),'.', '-') + 'T00:00:00</birthdate3>
<birthdate>' + replace(convert(varchar, c.DateOfBirth,102),'.', '-') + 'T00:00:00</birthdate>
<firstname>' + rtrim(ltrim(isnull(c.FirstName, ''))) + '</firstname>
<lastname>' + rtrim(ltrim(isnull(c.LastName, ''))) + '</lastname>
<priznak>' + case when a.Male = 1 then '1' else '2' end + '</priznak>' as request
	 from dbo.Child c	 
		inner join dbo.Request r on r.Id = c.RequestId
		inner join dbo.Applicant a on a.Id = r.ApplicantId
	 where r.YearOfRestId=5 and r.StatusId=1075


    OPEN childs  
    FETCH NEXT FROM childs INTO @request  


	begin tran
    WHILE @@FETCH_STATUS = 0  
    BEGIN  
		if @request is not null 
		begin 
			set @nguid = NEWID()
			set @number = '2064-9000063-048003-' + right('000000' + cast(@index as varchar),6) + '/17'

			insert into dbo.ExchangeBaseRegistry
			([RequestGuid], [ServiceNumber], [OperationType], [IsIncoming], [IsProcessed], [ExchangeBaseRegistryTypeId], [LastUpdateTick], [IsAddonRequest], [RequestText])
			select @nguid, @number, 'SendTask', 0, 0, 22, 0, 0, '<?xml version="1.0" encoding="utf-16"?>
			<CoordinateTaskMessage xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
			  <ServiceHeader>
				<FromOrgCode xmlns="http://asguf.mos.ru/rkis_gu/coordinate/v5/">2064</FromOrgCode>
				<ToOrgCode xmlns="http://asguf.mos.ru/rkis_gu/coordinate/v5/">1111</ToOrgCode>
				<MessageId xmlns="http://asguf.mos.ru/rkis_gu/coordinate/v5/">' + @nguid + '</MessageId>
				<ServiceNumber xmlns="http://asguf.mos.ru/rkis_gu/coordinate/v5/">' + @number + '</ServiceNumber>
				<RequestDateTime xmlns="http://asguf.mos.ru/rkis_gu/coordinate/v5/">2018-07-20T11:59:04.0662935+03:00</RequestDateTime>
			  </ServiceHeader>
			  <TaskMessage>
				<Task xmlns="http://asguf.mos.ru/rkis_gu/coordinate/v5/">
				  <RequestId>' + @nguid + '</RequestId>
				  <ValidityPeriod xsi:nil="true" />
				  <Responsible>
					<LastName>Мартынова</LastName>
					<FirstName>Инна</FirstName>
					<MiddleName>Викторовна</MiddleName>
					<JobTitle>начальник Управления реализации программ в сфере семьи и детства</JobTitle>
					<Phone>+7 (495) 777-77-77</Phone>
					<Email>MARTINOVAIV@MOS.RU</Email>
				  </Responsible>
				  <Department>
					<Name>Департамент культуры города Москвы</Name>
					<Code>2064</Code>
					<Inn>7702155262</Inn>
					<Ogrn>1027739805180</Ogrn>
					<RegDate xsi:nil="true" />
				  </Department>
				  <ServiceNumber>' + @number + '</ServiceNumber>
				  <ServiceTypeCode>048003</ServiceTypeCode>
				</Task>
				<Data xmlns="http://asguf.mos.ru/rkis_gu/coordinate/v5/">
				  <DocumentTypeCode>22</DocumentTypeCode>
				  <Parameter>
					<ServiceProperties xmlns="">' + @request + '
					</ServiceProperties>
				  </Parameter>
				  <IncludeXmlView>true</IncludeXmlView>
				  <IncludeBinaryView>false</IncludeBinaryView>
				</Data>
			  </TaskMessage>
			</CoordinateTaskMessage>'

			set @index = @index + 1
		end
		FETCH NEXT FROM childs INTO @request  
	end

CLOSE childs;  
DEALLOCATE childs;


select top 15* from dbo.ExchangeBaseRegistry 
order by Id desc

rollback tran