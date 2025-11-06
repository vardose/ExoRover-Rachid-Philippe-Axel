# ExoRover-Rachid-Philippe-Axel

## Schéma architectural
<img src="assets/Schema.jpg">

## Jusitification du shcéma
Nous avons choisi ce schéma architectural car il est simplifié pour éviter une architecture trop complexe dès le départ.

Nous avons décider de rendre le module “Mission Control” Instable car il est le module le plus a même d’être modifié. Pour ce qui est du module “Map”, celui-ci est au contraire stable et abstrait puisque les autres modules communique avec celui-ci comme une bibliothèque partagée.

Enfin, pour le module “Rover”, celui-ci est a moitié stable sans que cela pose problème car dans le contexte de ce projet, une fois lancé, le rover deviens maximalement stable.