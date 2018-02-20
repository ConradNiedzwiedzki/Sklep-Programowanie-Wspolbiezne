# Sklep-Programowanie-Wspolbiezne
Sklep na zaliczenie przedmiotu programowanie współbieżne, realizujący problem synchronizacji wątków (klientów i magazyniera) oraz pamięci współdzielonej (półek) semaforami.

1.	Treść zadania
W sklepie jest n regałów, na każdym regale k rodzajów produktów w określonej cenie, każdego rodzaju produktu jest m sztuk. Klienci robiąc zakupy zmniejszają liczbę produktów na pułkach regałów. W momencie, gdy zabraknie jakiegoś produktu na regale, zatrudniony magazynier uzupełnia jego ilość blokując przy tym regał dla kupujących. W programie są:
•	klienci o ograniczonej cierpliwości (czekają tylko określony czas na magazyniera i rezygnują);
•	klienci zdeterminowani, którzy czekają aż magazynier uzupełni regał produktami;

2.	Krótki opis problemu
Głównym problem w zadaniu jest zaimplementowanie wątków klientów oraz magazyniera w taki sposób, aby pobieranie produktów z półki nie mogło odbywać się w tym samym czasie, co ich uzupełnianiem na półkach. 
Drugim problemem jest implementacja dwóch rodzajów klientów: cierpliwych oraz niecierpliwych, a także dostawcy, który uzupełnia braki w produktach, gdy ilość produktu na półce spadnie do zera.
Ze względu na przejrzystość pracy ilość klientów zredukowano do jednego klienta przypadającego w danym momencie na jedną półkę. Z tego samego powody ilość półek zredukowano do trzech.

3.	Wykaz zasobów współdzielonych:
•	Ekran;
•	Półka nr 1;
•	Półka nr 2;
•	Półka nr 3;

4.	Wykaz wyróżnionych sekcji krytycznych:
•	Dodawanie klientów;
•	Rysowanie;
•	Uzupełnianie produktu;
•	Kupowanie produktu;

5.	Wykaz obiektów synchronizacji:
•	Semafor binarny odpowiadający za synchronizacje dodawania klientów do listy
oraz rysowania klientów znajdujących się na danej liście;
•	Trzy semafory binarne odpowiadające za wywłaszczanie półki dla magazyniera podczas uzupełniania produktów na określonym regale;

6.	Wykaz procesów sekwencyjnych:
•	Proces odpowiadający za poruszanie się klienta;
•	Proces odpowiadający za dodawanie klientów;
•	Proces odpowiadający za poruszanie się magazyniera;
