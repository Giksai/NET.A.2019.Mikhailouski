use northwind;

select count(Companies.CompanyName) as 'CustomerCount', Countries.CountryName as 'country'
from Companies inner join Countries 
on Companies.City=Countries.City 
group by Countries.CountryName
having count(Companies.CompanyName)>1
order by CustomerCount desc;