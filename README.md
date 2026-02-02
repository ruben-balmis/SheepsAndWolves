# SheepsAndWolves

![Foto Titulo](/SheepsAndWolfs/SheepsAndWolfs/html/fotoOvejasLobos.png)

**Sheeps&Wolves** es una practica realizada en la asignatura de Programación de 1ºDAW hecha con C#.

## Pasos para probar la aplicación
1. Descargar la carpeta SheepsAndWolves del repositorio
2. Abrir la soluncion del proyecto (`SheepsAndWolfs.sln`) con Visual Studio Community
3. Ejecuar el codigo dentro de Visual Studio Community (F5 o flecha verde arriba de visual studio)
4. Abrir el archivo `index.html` que se encuentra en la ruta `SheepsAndWolfs\SheepsAndWolfs\html`
5. Una vez dentro de la web, pulsar el boton de iniciar/detener para iniciar o detener la simulación
6. Para modificar el numero de cualquier objeto de la simulación (ovejas, lobos, agua, cesped y obstaculos), se puede cambiar en la función `GenerateWorld` dentro de la ruta `SheepsAndWolfs\SheepsAndWolfsEngine\Utils.cs`

## En que consiste SheepsAndWolves
La aplicacion de **Sheeps&Wolves** consiste en una simulacion entre ovejas y lobos en un tablero. En el tablero habran ovejas, lobos, obstaculos(rocas), cesped y agua, todos tienen su misión en esta simulación menos los obstaculos, obviamente, que están para estorbar. Te explico la logica detras de cada objeto:

* `Agua`: El agua sirve para que las ovejas y los lobos beban para saciar el nivel de sed.
* `Cesped`: El cesped sirve para que las ovejas se alimenten para saciar el nivel de hambre.
* `Lobo`: El lobo se encarga de buscar y perseguir a las ovejas para comer.
* `Oveja`: La oveja vive tranquilamente en el mundo simulado.

## Lógica de la IA
Hablemos de la lógica que hay detras de la IA de las ovejas y los lobos.
Las ovejas y los lobos tienen 4 estadisticas, `Hunger`, `Thirst`, `Energy` y `Health`, todas estas tienen un valor maximo de 100 puntos, las estadisticas de `Thirst` y `Energy` funcionan igual tanto para las ovejas como los lobos, la `Thirst` se recupera bebiendo agua, si baja de 0, el animal empieza a perder vida y la `Energy` se recupera descansando, si baja de cierto nivel, se fuerza un descanso y pierde los turnos.
La `IA` detrás de cada animal es un sistema de votaciones para ver que acción hace cada animal, las acciones son `MOVE_UP`, `MOVE_DOWN`, `MOVE_LEFT`, `MOVE_RIGHT`, `REST`, `DRINK`, `ATTACK`, `EAT`, algunas acciones como `REST`, `DRINK`, `ATTACK`, `EAT` requieren ciertas condiciones, como estar encima del agua para beber por ejemplo. Si todas las estadisticas estan bien, todas las acciones de movimiento tendran los mismos puntos de votacion y se elegira una aleatoria. Si una de las estadisticas esta por debajo de la mitad, se calcula en que dirección está y esa direccion tendra mas votos que el resto, por lo que habrá mas posibilidades que salga esa acción, así hasta que este en el lugar donde puede realizar la acción.
Hablemos de cada animal.
-- `Oveja`: La oveja se mueve por el mundo de forma aleatoria, si tiene sed irá hacia una casilla con agua para beber y si tiene hambre, irá hacia una casilla con cesped para comer. Si esta cansada se quedará quieta descansando para recuperar energia. Si un lobo se acerca a una oveja, la oveja intentará uir del lobo sumando puntos a la accion de moverse en dirección contraria.
-- `Lobo`: El lobo tambíen se mueve de manera aleatoria hasta que una sus estadisticas baje. Si el hambre baja, perseguirá a una oveja para al estar en la casilla adyacente, poder ejecutar la acción de `ATTACK` para bajar la vida a la oveja hasta matarla, una vez la oveja a muerto se puede consumir el cuerpo para recuperar el hambre hasta un total de 3 veces por cadaver. Si la sed o la energia bajan, hará lo mismo que la oveja para recuperar estas dos.
