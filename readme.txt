README EN
Main repository folder contains "Project" folder with Unity 6 game project. "Solitude" is a prototype currently in development, with most of the core mechanics and tools already fully functional.

GAME CORE PRINCIPLES:
- Tactical, turn-based gameplay on grid.
- "Reverse Tetris" feeling - hordes of enemies are coming from the top of the screen and we need to get rid of them as fast as possible with area spells.
- Tower defence game elements (more may be added yet).
- Play as an eccentric wizard whose lonely tower happens to be in the way of the army of darkness. Out of my lawn!

IMPLEMENTED MECHANICS, SYSTEMS AND TOOLS:
- Functional gameplay loop and progression.
- Database of objects utilizing and inheriting from Unity libraries, integrated with a save system, enabling persistent storage of such complex data types on disk - A custom serialization system designed to bypass the limitations 
	of Unity’s built-in serialization
-Scriptable Object and Unity Editor based system for game and level designers to create content without coding:
	- Ability to create "fixed" and randomized level presets.
	- Ability to create spells and equipment items.
	- Ability to create enemies with various stats and behaviors.
- Debug Mode / God Mode for designers, with a graphical interface that allows:
	- Generating maps of any size.
	- Adding or replacing enemies during gameplay.
	- Testing and swapping spells or equipment without restrictions.
	- To enable Debug Mode in Play Mode, check the "Debug Mode" checkbox in the MainMenu scene’s GameController.
- Cross-platform universal controls for both mobile and desktop, based on the new Unity Input System package.
- Smarter enemy behaviors implemented with graph-based pathfinding algorithms.
- Game map system with tiles capable of storing enemies, obstacles etc., with complex interactions between objects on map – similar to systems used in larger strategy games.
-Configurable enemy spawner that among others allows to generate different enemies each time while maintaining difficulty and theme of the level. Prevents oversaturation of a single enemy type in a game turn 
and in gameplay in general.
- Turn-based gameplay system built on a state machine architecture.

GAMEPLAY MECHANICS:
	-Spells – offensive, obstacle-creating, and debuffing abilities.
	-Enemies – diverse stats, behaviors, and the ability to buff one another.
	-Attribute systems for player and enemies.
	-Inventory and item systems. Three main item types: Large Scrolls, Potions and Small Scrolls. Some items can be used instead of the main action in a turn, others as secondary action.
	-Loot system.

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Redame PL
Główny folder repozytorium zawiera folder "Project" z projektem Unity 6 gry. "Solitude" jest prototypem gry w trakcie tworzenia z większością głownych mechanik i narzędzi w pełni działających.

Założenia gry:
- Taktyczna, turowa rozgrywka na siatce (kafelki).
- Założenie "odwróconego Tetrisa" - hordy wrogów nadciągają z góry mapy, a my czyścimy je zaklęciami obszarowymi o konkretnych kształtach. 
- Rozgrywka ma elementy "Tower-Defence" (których w przyszłości może być dodanych więcej). 
- Wcielamy się w ekscentrycznego czarodzieja, którego wieża znalazła się na drodze armii ciemności.

Zaimplementowane mechaniki, systemy i narzędzia:
- Działający gameplay loop i progresja rozgrywki. 
- Baza danych obiektów korzystających i dziedziczących z bibliotek Unity, zintegrowana z systemem zapisu, umożliwiająca trwałe przechowywanie takich złożonych typów danych na dysku - Autorski system serializacji zaprojektowany w celu 
	obejścia ograniczeń wbudowanej serializacji Unity.
- Bazujący na Scriptable Objectach i Edytorze Unity system do pracy dla game i level designerów do tworzenia contentu, niewymagjący programowania:
	- Możliwość tworzenia "sztywnych" i randomizowanych presetów poziomów. 
	- Możliwość tworzenia zaklęć i przedmiotów ekwipunku.
	- Możliwość tworzenia wrogów o różnych statystykach i zachowaniach. 
- Tryb "Debug Mode", "God Mode" dla game i level designerów z graficznym interfejsem umożliwiający generowanie mapy dowolnej wielkości, ustawianie i podmienianie przeciwników w trakcie rozgrywki oraz testowanie
i podmienianie zaklęć oraz przedmiotów ekwipunku bez limitu. Aby dowolny poziom uruchomił się w trybie "Debug Mode" w Play Modzie wystarczy na scenie MainMenu w głównym kontrolerze gry zaznaczyć checkboxa "Debug Mode". 
Tryb ma za zadnie ułatwić tworzenie i testowanie koncepcji rozgrywki.
- Uniwersalne sterowanie dla urządzeń mobilnych i desktopowych bazujące na nowej paczce Unity Input System.
- Zachowania bardziej inteligentnych wrogów bazują na algorytmach wyszukiwania ścieżek na grafach.
- System mapy z kafelkami mogącymi przechowywać obiekty typu wrogowie, przeszkody etc. i rozbudowancy interakcji między obiektami poruszającymi się po mapie, analogiczny do stosowanych w większych grach strategicznych.
- Konfigurowalny spawner wrogów pozwalający m. in. za każdym razem generować trochę innych wrogów przy zachowaniu stopnia trudności i unikać nadmiernej koncentracji jednego typu wrogów w danej turze/turach.
- System turowej rozgrywki bazujący na maszynie stanów.

- Mechaniki rozgrywki:
	- Zaklęcia (ofensywne, tworzące przeszkody i nakładające statusy na wrogów). 
	- Wrogowie - mają różne zachowania, statystyki i mogą buffować się nawzajem.
	- System atrybutów gracza i wrogów.
	- System ekwipunku gracza i system przedmiotów. Trzy główne typy przedmiotów: Duże Zwoje, Mikstury i Małe Zwoje. Niektóre przedmioty mogą być użyte zamiast akcji głównej w turze (Duże Zwoje), a inne w ramach akcji dodatkowej.
 	- System lootu.


 
