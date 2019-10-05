use northwind;

select count(company), city from customers where city in('Las Vegas', 'Miami', 'Portland') group by city; 