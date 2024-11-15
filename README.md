Redame i dokumentacja PL

Główny folder repozytorium zawiera folder "Project" z projektem Unity LTS 2022.3.20f1 "Solitude Of The Firestorm". 
"Solitude Of The Firestorm" jest prototypem gry w trakcie developementu. 

Dokumentacja na Miro:
https://miro.com/welcomeonboard/amJJaU5CVlpBT2l1YUpwc01XZzlIdFl3dmVMUnQ3S0l1ZjhlalBGYlRjbndPc3dORUNROC9OVDVyakJSdzd3ak1vY2xsRGdqRDVHb29SbWtuYVJuMzl0Ujk1M2NyalpxRUVGUTJJTDJEMGVkY0ZvSFIySkR4aWd1b0lPczBIRmUhZQ==?share_link_id=630813919850

Założenia gry:
- Taktyczna rozgrywka
- Rozgrywka turowa
- Rozgrywka na siatce (kafelki)
- Założenie "odwróconego Tetrisa" - hordy wrogów nadciągają z góry mapy, a my czyścimy je zaklęciami obszarowymi o konkretnych kształtach 
- Rozgrywka ma elementy "Tower-Defence" 
- Wcielamy się w ekscentrycznego czarodzieja którego wieża znalazła się na drodze armii ciemności.

Gameplay:
1) Tura Wrogów - wrogowie pojawiają się u góry ekranu, idą w dół ekranu lub jeśli znaleźli się u samego dołu ekranu, 
zadają graczowi obrażenia. Wrogowie posiadający aury buffują wrogów w zasięgu aury. Gracz przegrywa, jeśli straci wszystkie punkty życia.
Podstawowi wrogowie idą w dół jeśli kafelek jest wolny i czekają, jeśli nie jest. Bardziej zaawansowani wrogowie mogą obchodzić przeszkody. 
2) Tura Gracza - gracz losuje na początku tury 2 zaklęcia i wybiera jedno. Będzie ono jego główną akcją w turze. Gracz ma ograniczoną
ilość przelosowań tych zaklęć w ramach poziomu gry. 
Gracz ma do dyspozycji 3 typy przedmiotów:
-Duże Zwoje - zamieniają zaklęcie w głównej akcji gracza na potężniejsze, często o globalnym zasięgu.
- Mikstury - buffują gracza na określoną ilość tur, np. dajac bonus do obrażeń lub zasięgu zaklęć.
- Małe zwoje - pozwalają rzucić dodatkowe słabsze zaklęcie poza główną akcją gracza.
Gracz może mieć po 3 przedmioty każdego typu w ekwipunku. Przedmioty są usuwane z ekwipunku po użyciu. Różne typy wrogów mają różną
szansę na pozostawienie na kafelku określony typów przedmiotów. Przedmiot jest zbierany z kafelka, gdy ten znajdzie się w obszarze
działania zaklęcia gracza.  

Typy zaklęć:
Wszystkie zaklęcia mają określony zasięg od krawędzi ekranu gracza, w jakim mogą być rzucone.
Zaklęcia dzielą się na typy:
1) Obszarowe - mają określony zasięg i obszar działania. Domyślne zaklęcie głównej akcji gracza jest najczęściej zaklęciem obszarowym.  
Dzielą się na:
- Zaklęcia obszarowe ofensywne - zadają wrogom w zasięgu rażenia obażenia i/lub nakładają na nich status/statusy.
- Zaklęcia obszarowe tworzenia - tworzą przeszkodę na czas x tur. Przeszkody dzielą się na wyłączające kafelek całkowicie z pathfindingu
wrogów lub zadające wrogom obrażenia i/lub nakładające na nich status/statusy. 
2) Globalne - działają na wszystkie kafelki na mapie. Z reguły dostępne tylko na Dużych Zwojach. 

Zaimplementowane mechaniki i systemy:
- GameController.cs - zarządza rozgrywką, na wzorcu maszyny stanów.
- Player.cs - zarządza statystykami, stanami i akcjami gracza. Na wzorcu maszyny stanów. 
- Tile.cs - Kafelek - zarządza obiektami, które mogą się na nim znajdować równocześnie i zwraca te obiekty dla innych systemów
oraz przekazuje tym obiektom operacje, jakie mają na sobie wykonać, otrzymane od innych systemów. 
- Map.cs - plansza rozgrywki, określa wielkość planszy, przechowuje wszystkie kafelki i zawiera matody incjalizujące mapę. 
- LevelEnemiesPresetGenerator - generuje losowy preset fal wrogów dla levelu. Pozwala na określenie typów wrogów, jacy maja znaleźć się
w levelu, ilość wrogów każdego tieru oraz minimalną i maksymalną odległość między wrogami w osi x.
- LevelEnemiesPreset.cs - gotowy preset fal wrogów dla levelu. Scriptalbe wrogów można przeciągać do tablicy dwuwymiarowej wyciągniętej do 
inspektora. Pola będące nullami są odstępami między wrogami.
- LevelLoader.cs - odpowiada za inicjalizację levelu, głównie fal wrogów. Może załadować gotowy preset wrogów lub losowy preset 
z generatora.
- Spawner.cs - sprawdza, czy jest miejsce na zespawnowanie kolejnych wrogów z załdowanej puli i jeśli tak, umieszcza ich u góry mapy. 
- GameplayObject.cs - klasa bazowa dla wszystkich obiektów mogących znajdować się na kafelku 
- BaseEnemy.cs - klasa bazowa dla wrogów. Dziedziczone są z niej podstawowe atrybuty, podstawowe interakcje i pole tieru wroga.
- CollectableItem.cs - przedmiot do zebrania na kafelku
- Item.cs - klasa bazowa dla przedmiotów w ekwipunku gracza
- Obstacle.cs - klasa przeszkody. Określa czy przeszkoda blokuje kafelek dla pathfindingu i jej czas istneinia w turach.
Jeśli przeszkoda nie blokuje wejścia na kafelek określa jakie obrażenia i/lub statusy zadaje wrogom. 
- EnemiesController.cs - zarządza aktywnymi wrgami 
- ObstaclesController.cs - zarządza aktywnymi przeszkodami
- BaseAttribute.cs - klasa bazowa dla atrybutu. Określa ID atrybutu, jego nazwę oraz zawiera metody przyjmujące i obsługujące status 
dla danego atrybutu. Wrogowie mają 5 atrybutów: AttributeHealth.cs, AttributeMovementSpeed.cs, AttributeDamage.cs, 
AttributeNotImmobilised.cs, AttributeDefence.cs     
- Status.cs - zawiera atrybut którego dotyczy status.
- Spawner.cs - scriptable object który może być przeciągany na scriptable object wroga. Określa jakie przedmioty i z jakim poziomeme 
prawdopodobieństwa mogą pojawić się na kafelku po śmierci wroga. 
