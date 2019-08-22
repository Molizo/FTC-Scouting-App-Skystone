const CACHE = "ftcsoutingapp";

const offlineFallbackPage = "/error/offline.html";

const offlineResources = [
    '/error/offline.html',
    '/css/style.css',
    '/css/materialize.css',
    '/css/themes/theme-quantum.css',
    '/plugins/animate-css/animate.css',
    '/vendor/node-waves/dist/waves.css',
    'https://cdnjs.cloudflare.com/ajax/libs/cookieconsent2/3.1.0/cookieconsent.min.css',
    'https://cdnjs.cloudflare.com/ajax/libs/cookieconsent2/3.1.0/cookieconsent.min.js',
    'https://stackpath.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css',
    'https://stackpath.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js',
    'https://code.jquery.com/jquery-3.4.1.min.js',
    'https://fonts.googleapis.com/css?family=Roboto:400,700&subset=latin,cyrillic-ext',
    'https://fonts.googleapis.com/icon?family=Material+Icons',
    '/vendor/jquery-slimscroll/jquery.slimscroll.js',
    '/vendor/node-waves/dist/waves.js',
    '/js/admin.js',
    '/js/demo.js',
    '/media/user-img-background.png',
    '/favicon.ico',
    '/manifest.json',
    '/service_worker.js',
    '/Docs',
    '/Docs/Events',
    '/Docs/Teams',
    '/Docs/OfficialMatches',
    '/Docs/ScoutedMatches',
    '/Legal'
]

// Install stage sets up the offline page in the cache and opens a new cache
self.addEventListener("install", function (event) {
    console.log("[FTC Scouting App] Install Event processing");

    event.waitUntil(
        caches.open(CACHE).then(function (cache) {
            console.log("[FTC Scouting App] Cached website resources");

            return cache.addAll(offlineResources);
        })
    );
});


// If any fetch fails, it will show the offline page.
self.addEventListener("fetch", function (event) {
    if (event.request.method !== "GET") return;

    event.respondWith(
        // Try the cache
        caches.match(event.request).then(function (response) {
            return response || fetch(event.request);
        }).catch(function (error) {
            // The following validates that the request was for a navigation to a new document
            if (
                event.request.destination !== "document" ||
                event.request.mode !== "navigate"
            ) {
                return;
            }

            console.error("[FTC Scouting App] Network request Failed. Serving offline page " + error);
            return caches.open(CACHE).then(function (cache) {
                return cache.match(offlineFallbackPage);
            });
        })
    );
});

// This is an event that can be fired from your page to tell the SW to update the offline page
self.addEventListener("refreshOffline", function () {
    const offlinePageRequest = new Request(offlineFallbackPage);

    return fetch(offlineFallbackPage).then(function (response) {
        return caches.open(CACHE).then(function (cache) {
            console.log("[FTC Scouting App] Offline page updated from refreshOffline event: " + response.url);
            return cache.put(offlinePageRequest, response);
        });
    });
});