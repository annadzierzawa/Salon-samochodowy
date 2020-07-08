-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Czas generowania: 08 Lip 2020, 15:47
-- Wersja serwera: 10.1.44-MariaDB-0+deb9u1
-- Wersja PHP: 7.4.7

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `SalonSamochodowy`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Pracownicy`
--

CREATE TABLE `Pracownicy` (
  `idPracownika` int(11) NOT NULL,
  `login` varchar(20) NOT NULL,
  `password` varchar(255) NOT NULL,
  `imie` text NOT NULL,
  `nazwisko` text NOT NULL,
  `premia` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Zrzut danych tabeli `Pracownicy`
--

INSERT INTO `Pracownicy` (`idPracownika`, `login`, `password`, `imie`, `nazwisko`, `premia`) VALUES
(1, 'wlasciciel', 'wlasciciel', 'Jan', 'Kowalski', 1000),
(3, 'psilva', 'piesek', 'Pawcio', 'Silva', 0),
(5, 'anowak', 'anowak', 'Anna', 'Nowak', 0),
(8, 'pizza', 'pizza', 'Arturito', 'Pizza', 0),
(9, 'hiry', 'hiry', 'Hubert', 'Irygator', 0),
(10, 'bettyd', 'bettyd', 'Beata', 'Gucio', 0),
(12, 'amerc', 'amerc', 'Adriano', 'Mercedes', 0),
(13, 'skotlow', 'skotlow', 'Szymon', 'Kotłownia', 0);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Samochody`
--

CREATE TABLE `Samochody` (
  `idModelu` int(11) NOT NULL,
  `marka` text NOT NULL,
  `model` text NOT NULL,
  `silnik` text NOT NULL,
  `moc` int(11) NOT NULL,
  `kolor` enum('Czarny','Czerwony','Niebieski','Zielony','Metalic','Szary','Złoty','Oliwkowy','Custom') NOT NULL,
  `krajProdukcji` text NOT NULL,
  `dataProdukcji` year(4) NOT NULL,
  `cenaModelu` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Zrzut danych tabeli `Samochody`
--

INSERT INTO `Samochody` (`idModelu`, `marka`, `model`, `silnik`, `moc`, `kolor`, `krajProdukcji`, `dataProdukcji`, `cenaModelu`) VALUES
(2, 'Mercedes', 'AMG', '3.5 CDI', 200, 'Zielony', 'Chiny', 2019, 250000),
(19, 'Ferrari', 'Testarossa', '4.7 Benzyna', 380, 'Czerwony', 'Włochy', 1982, 512000),
(21, 'Nissan', 'Juke', '1.6 CDI', 120, 'Czarny', 'Japonia', 2018, 76000),
(23, 'Mercedes', 'C63 AMG', '2.2 CDI', 450, 'Zielony', 'Turcja', 2008, 130000),
(24, 'Mercedes', 'C43', '1.9 JTD', 850, 'Niebieski', 'Armenia', 2018, 350000),
(25, 'Ford', 'Mustang', '5.0', 390, 'Zielony', 'USA', 2020, 85000),
(26, 'Fiat', 'Tipo', '1.4 JetV', 115, 'Czerwony', 'Włochy', 2016, 65000),
(27, 'Citroen', 'C4', '1.2 Benzyna', 65, 'Czerwony', 'Włochy', 2010, 5100),
(29, 'Polonez', 'Caro', '1.8 LPG', 90, 'Czerwony', 'Polska', 1994, 12000),
(30, 'Audi', 'R8', '5.2 TFSI', 610, 'Czarny', 'Belgia', 2020, 730500),
(31, 'DMC', 'DeLorean', '3.4', 280, 'Zielony', 'USA', 1988, 89000),
(35, 'Lamborgini', 'Gallardo', 'V12', 1000, 'Zielony', 'Włochy', 2010, 1000000),
(37, 'Volvo', 'S 90', '2.5', 340, 'Niebieski', 'Szwecja', 2018, 6500),
(38, 'Wołga', 'Gangsterska', '6.3', 540, 'Czarny', 'Polska', 1975, 46000),
(39, 'Fiat', 'Multipla', '1.6', 103, 'Czerwony', 'Polska', 2005, 23000),
(40, 'Ford', 'GT', '5.0', 666, 'Czarny', 'Polska', 1969, 65000),
(41, 'Opel', 'Astra', '1.5 TSI', 145, 'Czarny', 'POLSKA GLIWICE', 2018, 94000),
(42, 'Citroen', 'C3', '1.2', 70, 'Czerwony', 'Francja', 2003, 6500);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Sprzedaz`
--

CREATE TABLE `Sprzedaz` (
  `idSprzedazy` int(11) NOT NULL,
  `idPracownika` int(11) DEFAULT NULL,
  `idModelu` int(11) DEFAULT NULL,
  `cena` double DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Zrzut danych tabeli `Sprzedaz`
--

INSERT INTO `Sprzedaz` (`idSprzedazy`, `idPracownika`, `idModelu`, `cena`) VALUES
(15, 3, 2, 250000),
(16, 3, 25, 85000),
(17, 3, 23, 130000),
(18, 5, 29, 12000),
(19, 5, 19, 512000),
(20, 8, 21, 76000),
(21, 5, 30, 730500),
(23, 13, 29, 12000),
(24, 13, 30, 730500),
(25, 10, 19, 512000),
(26, 10, 26, 65000),
(27, 10, 2, 250000),
(28, 9, 23, 130000),
(29, 9, 24, 350000),
(30, 9, 29, 12000),
(31, 9, 27, 5100),
(32, 1, 23, 130000),
(35, 1, 30, 730500),
(36, 1, 30, 730500),
(40, 3, 38, 46000),
(41, 3, 40, 65000),
(42, 3, 26, 65000),
(43, 1, 41, 94000);

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `Pracownicy`
--
ALTER TABLE `Pracownicy`
  ADD PRIMARY KEY (`idPracownika`);

--
-- Indeksy dla tabeli `Samochody`
--
ALTER TABLE `Samochody`
  ADD PRIMARY KEY (`idModelu`);

--
-- Indeksy dla tabeli `Sprzedaz`
--
ALTER TABLE `Sprzedaz`
  ADD PRIMARY KEY (`idSprzedazy`),
  ADD KEY `idPracownika` (`idPracownika`),
  ADD KEY `idModelu` (`idModelu`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT dla tabeli `Pracownicy`
--
ALTER TABLE `Pracownicy`
  MODIFY `idPracownika` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT dla tabeli `Samochody`
--
ALTER TABLE `Samochody`
  MODIFY `idModelu` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=43;

--
-- AUTO_INCREMENT dla tabeli `Sprzedaz`
--
ALTER TABLE `Sprzedaz`
  MODIFY `idSprzedazy` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=44;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
