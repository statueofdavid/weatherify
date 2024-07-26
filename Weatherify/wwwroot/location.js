// https://www.learnblazor.com/javascript-interop

function getLocation() {
	return new Promise((resolve, reject) => {
          navigator.geolocation.getCurrentPosition((position) => {
            console.log(`Latitude: ${position.coords.latitude}, Longitude: ${position.coords.longitude}`);
	    resolve({
              coords: {
                latitude: position.coords.latitude,
	        longitude: position.coords.longitude
	      }
	    });
          }, (error) => {
            reject(error);
          });
       });
}
