use northwind;
select company from customers where city in('Boise', 'New York', 'Madrid', 'Portland', 'Miami') order by company desc;

-- Берлин, Лондон, Мадрид, Брюссель, Париж.