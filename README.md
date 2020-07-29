# Algorytm-najszybszego-spadku-
Program oblicza minimum lokalne zadanej różniczkowalnej funkcji matematycznej korzystając z algorytmu najszybszego spadku

## Omówienie działania programu
Aplikacja została napisana w języku C# z wykorzystaniem platformy .NET framework 4.6.1. Interfejs użytkownika został napisany w środowisku WPF. Interfejs pozwala na wprowadzenie danych określonych w założeniach projektowych. Program w celu prowadzenia obliczeo na zadanej funkcji wykorzystuje mXPrarser[1].

<h3> 3.1. Wprowadzanie danych </h3>
Po uruchomieniu programu wszystkie pola mają wpisane odpowiednie wartości domyślne, które użytkownik może w swobody sposób zmieniad. Podczas wpisywania funkcji należy podad ilośd parametrów wejściowych po lewej stronie równania oraz treśd równania po prawej stronie np.. „ f(x1,x2) = x1+x2” a następnie wcisnąd przycisk „zapisz” (użytkownik zostanie poinformowany jeśli zapisana składnia będzie nieprawidłowa). Następnie należy wpisad parametry wejściowe algorytmu: punkt startowy, tau, beta oraz epsilon1-3 lub pozostawid wartości domyślne. Po wybraniu przycisku „Oblicz’ program oblicza minimum funkcji oraz pozwala na wyświetlenie warstwicy pod warunkiem, że funkcja posiada dwie zmienne wejściowe.
<h3> 3.2. Wyświetlanie wyników</h3> 

* Po zakooczeniu obliczania wyświetlony zostaje odpowiedni komunikat o znalezionym punkcie wraz z informacją czy jest to minimum lokalne funkcji czy jest program utknął nie potrafiąc znaleźd ekstremum.
* W tabeli zostaną wyświetlane wszystkie obliczone punkty, wraz z ich argumentami oraz wartością funkcji.
* Jeśli funkcja posiada dwie zmienne to istnieje możliwośd narysowania warstwicy poprzez wciśnięcie przycisku „warstwica” ( o zadanej wielkości). Warstwica prezentowana jest poprzez odpowiednie kolory (niebieski dla małych wartości, czerwony dla wysokich wartości, zielony dla środkowych wartości). W oknie warstwicy można narysowad nową warstwicę o innych parametrach niż domyślne (należy pamiętad, że im dokładniejsza warstwica, tym dłużej trwa jej rysowanie). Na warstwice nałożone są kolejne obliczone punkty przez algorytm połączone prostą linią.

 <h5>W celu prowadzenia obliczeń na funkcji zadanej przez użytkownika użyty został mXparser </h5>
(http://mathparser.org/) 
(http://mathparser.org/mxparser-license/)
