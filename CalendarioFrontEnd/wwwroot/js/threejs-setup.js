﻿// wwwroot/js/threejs-setup.js
function initializeThreeJs() {

    var scene = new THREE.Scene();
    var camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
    var renderer = new THREE.WebGLRenderer();
    renderer.setSize(window.innerWidth, window.innerHeight);


    document.getElementById("threejs-container").appendChild(renderer.domElement);

    var geometry = new THREE.BoxGeometry();
    var material = new THREE.MeshBasicMaterial({ color: 0x00ff00 });
    var cube = new THREE.Mesh(geometry, material);
    scene.add(cube);

    camera.position.z = 5;


    var animate = function () {
        requestAnimationFrame(animate);

 
        cube.rotation.x += 0.01;
        cube.rotation.y += 0.01;


        renderer.render(scene, camera);
    };

    animate();
}
