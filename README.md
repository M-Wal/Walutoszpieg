# Walutoszpieg - Sledź i zarządaj walutami w łatwy sposób. Sprawdzaj bieżące kursy aby podjąć właściwe decyzje.

1.	Cel aplikacji: 
Walutoszpieg ma na celu dostarczenie użytkownikom narzędzi do śledzenia aktualnych kursów walut, analizowania krótkoterminowych trendów, zarządzania portfelem walutowym oraz rejestrowania historii wymian walut. Aplikacja jest przeznaczona zarówno dla osób indywidualnych, które planują swój budżet wakacyjny jak również i małych firm, które chcą efektywnie zarządzać swoimi finansami międzynarodowymi.

2.	Kluczowe funkcje
• Aktualne kursy walut - Użytkownicy mogą sprawdzić najnowsze kursy walut, które są pobierane bezpośrednio z API Narodowego Banku Polskiego (NBP). Kursy są aktualizowane na bieżąco, co pozwala na podejmowanie świadomych decyzji finansowych.
• Krótkoterminowa analiza kursów walut - Aplikacja oferuje narzędzia do analizy krótkoterminowych trendów walutowych, dzięki czemu użytkownicy mogą monitorować wahania kursów i przewidywać ich przyszłe zmiany. Umożliwia to lepsze planowanie zakupów i sprzedaży walut.
• Portfel walutowy - Użytkownicy mogą stworzyć i zarządzać swoim portfelem walutowym, dodając różne waluty oraz monitorując ich ilość i wartość. Portfel jest przechowywany w bezpieczny sposób, a jego zawartość jest łatwa do przeglądania i aktualizacji.
• Historia wymian walut - Aplikacja umożliwia rejestrowanie wszystkich transakcji wymiany walut, co pozwala na śledzenie historii operacji finansowych. Użytkownicy mogą przeglądać szczegóły każdej transakcji, w tym datę, kurs wymiany, ilość i walutę.
• Analiza zmian kursu - Użytkownicy mogą sprawdzić rekomendacje, które będą informować o zmianach kursów walut. Pozwala to na szybką reakcję na korzystne zmiany na rynku walutowym.

3.	Korzyści dla Użytkowników:
•	Dostęp do aktualnych kursów walut - Użytkownicy mają dostęp do najnowszych kursów walut, co pozwala im na podejmowanie lepszych decyzji finansowych
•	Efektywne zarządzanie portfelem - Możliwość tworzenia i zarządzania portfelem walutowym pozwala użytkownikom na śledzenie wartości ich aktywów w różnych walutach
•	Śledzenie historii transakcji - Rejestracja i przegląd historii wymian walut pomaga użytkownikom w monitorowaniu ich operacji finansowych i analizowaniu ich wyników

4.	Technologie:
•	Platforma: Windows
•	Backend: C#
•	Frontend: React
•	Baza Danych: MSSQL

5.	Zestaw Funkcjonalności Walutoszpieg:
•	Śledzenie Kursów walut:
o	Aktualizacja kursów walut,
o	Prezentacja kursów dla popularnych walut.
•	Narzędzia Analityczne i Wizualizacja Danych:
o	Możliwość porównywania kursów różnych walut w wybranym okresie.
•	Zarządzanie Listą Ulubionych Walut:
o	Tworzenie i zarządzanie listą ulubionych walut dla szybkiego dostępu.
o	Szybkie przełączanie między różnymi użytkownikami.
•	Dodatkowe Funkcjonalności (opcjonalnie):
o	Konwerter Walut: Wbudowany kalkulator do szybkiego przeliczania wartości między różnymi walutami.

6. Instrukcja uruchomienia
• Stwórz bazę danych i dodaj przykładowych użytkowników - skrypt CreateDatabase.sql
• W katalogu Backend/Walutoszpieg zaktualizuj plik appsettings.json o prawidłowy connection string do bazy danych
• Uruchom i przetestuj backend
• W katalogu Frontend/Walutoszpieg/src/api zaktualizuj port serwera - sprawdź w swagger
• Uruchom frontend npm run dev i baw się do woli :)

-----------------------------------------------------------------------------------

7.	 Plan Realizacji Walutoszpieg:
Faza 1: Przygotowanie i Planowanie
a.	Analiza Wymagań
a.	Zdefiniowanie szczegółowych wymagań funkcjonalnych i niefunkcjonalnych.
b.	Stworzenie dokumentacji projektowej.
b.	Architektura Systemu
a.	Zdefiniowanie technologii i narzędzi (backend, frontend, bazy danych, zabezpieczenia).
b.	Stworzenie schematu architektury aplikacji.

Faza 2: Projektowanie
1.	Projekt Interfejsu Użytkownika (UI)
o	Stworzenie makiet i prototypów ekranów aplikacji.
2.	Planowanie Bazy Danych
o	Projektowanie struktury bazy danych.
o	Definiowanie schematów tabel i relacji.

Faza 3: Rozwój i Implementacja
1.	Rozwój Backend
o	Implementacja serwera i API do obsługi danych kursów walut.
o	Integracja z zewnętrznymi źródłami danych finansowych.
2.	Rozwój Frontend
o	Implementacja interfejsu użytkownika na platformie Windows.
o	Implementacja funkcji śledzenia kursów w czasie rzeczywistym i analiz.
3.	Gromadzenie Historii
o	Implementacja funkcji archiwizacji i przechowywania danych historycznych.

Faza 4: Testowanie
1.	Testy Jednostkowe i Integracyjne
o	Testowanie poszczególnych modułów aplikacji.
o	Testowanie integracji między backendem a frontendem.
2.	Testy Systemowe i Akceptacyjne
o	Testowanie aplikacji jako całości.

Harmonogram:
•	Faza 1: 1 dni
•	Faza 2: 1 dni
•	Faza 3: 4 dni
•	Faza 4: 2 dni

Projekt zakłada realizację aplikacji w ciągu 8 dni, z uwzględnieniem etapów planowania, projektowania, rozwoju, testowania.

MOSCOW:

1) Must-Have (Funkcje niezbędne):
Moduł użytkownika:
    Profil użytkownika (3 story point)
    Kalkulator walutowy (3)
Portfele walutowe:
    Możliwość dodawania i usuwania walut (3)
    Przeglądanie sald walutowych (1)
    Historia transakcji wymiany walut (3)
Wymiana walut:
    Przeliczanie kwot między różnymi walutami (5)
    Aktualne kursy walut pobierane z API NBP: (3) https://api.nbp.pl/api/exchangerates/tables/A?format=json
    Możliwość wykonania transakcji wymiany walut (5)
Alerty o zmianie kursu waluty:
Prezentowanie alertów dla wybranych walut (3)

2) Should-Have (Funkcje pożądane):
Wykresy historyczne kursów walut (8)
Powiadomienia o zmianach kursu waluty w aplikacji (3)

3) Could-Have (Funkcje opcjonalne):
Inne źródła kursów walut (5)
Powiadomienia o zmianach kursu waluty na email (3)