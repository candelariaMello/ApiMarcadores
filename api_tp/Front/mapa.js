//HERE MAPS:

// Initialize the platform object
var platform = new H.service.Platform({
  'apikey': '8M_tIG6pilYhB7pr9mD3mL0V28ACH-_yi2ynRKEbwPc'
});


// Obtain the default map types from the platform object
var maptypes = platform.createDefaultLayers();

// Instantiate (and display) the map
var map = new H.Map(
  document.getElementById('mapContainer'),
  maptypes.vector.normal.map,
  {
    zoom: 17,
    center: { lat: -31.44098, lng: -64.19296 } //-31.44098, -64.19296
  });

var ui = H.ui.UI.createDefault(map, maptypes, 'de-DE');

// Enable the event system on the map instance:
var mapEvents = new H.mapevents.MapEvents(map);

var behavior = new H.mapevents.Behavior(mapEvents)

// Add event listener:
map.addEventListener('tap', function (evt) {
  if(evt.target instanceof H.map.Marker){
    let bubble = new H.ui.InfoBubble(evt.target.getGeometry(),{
      content: evt.target.getData()
    });
    ui.addBubble(bubble);
  }
});

// Cuando se cargue el documento:
$(document).ready(function () {
  
  const obtenerMarcadores = () => {
    const url = "https://localhost:7122/api/marcadores/getMarcadores"
    fetch(url)
      .then(response => response.json())
      .then((response) => {
        
        response.forEach(element => {
          var marker = new H.map.Marker({ lat: element.latitud, lng: element.longitud });
          marker.setData(element.info);
          
          map.addObject(marker);
        });
      })
      .catch((error)=>{alert(error)})

  }
  obtenerMarcadores();
})


