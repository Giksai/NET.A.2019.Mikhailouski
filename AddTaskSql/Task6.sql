use northwind;
select first_name from customers where business_phone like '(123)%' and business_phone like '%555%'
	and fax_number like '(123)%101';