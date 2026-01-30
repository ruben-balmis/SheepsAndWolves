# SheepsAndWolves

![Foto Titulo](/SheepsAndWolfs/SheepsAndWolfs/html/fotoOvejasLobos.png)

**Sheeps&Wolves** es una practica realizada en la asignatura de Programación de 1ºDAW hecha con C#.

## Pasos para probar la aplicación
1. Descargar la carpeta SheepsAndWolves del repositorio
2. Abrir la soluncion del proyecto (`SheepsAndWolfs.sln`) con Visual Studio Community
3. Ejecuar el codigo dentro de Visual Studio Community (F5 o flecha verde arriba de visual studio)
4. Abrir el archivo `index.html` que se encuentra en la ruta `SheepsAndWolfs\SheepsAndWolfs\html`
5. Una vez dentro de la web, pulsar el boton de iniciar/detener para iniciar o detener la simulación

## En que consiste SheepsAndWolves
La aplicacion de **Sheeps&Wolves** consiste en una simulacion entre ovejas y lobos en un tablero. En el tablero habran ovejas, lobos, obstaculos(rocas), cesped y agua, todos tienen su misión en esta simulación menos los obstaculos, obviamente, que están para estorbar. Te explico la logica detras de cada objeto:

* `Agua`: El agua sirve para que las ovejas y los lobos beban para saciar el nivel de sed.
* `Cesped`: El cesped sirve para que las ovejas se alimenten para saciar el nivel de hambre.
* `Lobo`: El lobo se encarga de buscar y perseguir a las ovejas para comer.
* `Oveja`: La oveja vive tranquilamente en el mundo simulado.

## Lógica de la IA
Hablemos de la lógica que hay detras de la IA de las ovejas y los lobos.
Las ovejas y los lobos tienen 4 estadisticas, `Hunger`, `Thirst`, `Energy` y `Health`, todas estas tienen un valor maximo de 100 puntos, las estadisticas de `Thirst` y `Energy` funcionan igual tanto para las ovejas como los lobos, la `Thirst` se recupera bebiendo agua, si baja de 0, el animal empieza a perder vida y la `Energy` se recupera descansando, si baja de cierto nivel, se fuerza un descanso y pierde los turnos.
