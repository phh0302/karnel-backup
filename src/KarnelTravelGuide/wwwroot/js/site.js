// =====================================================================
// Karnel Travel Guide - small helpers used on several pages
// =====================================================================

// Replace a missing photo with the placeholder image (GUI Standards, section 6).
// Images opt in with the class "kt-img".
document.addEventListener("error", function (e) {
    var img = e.target;
    if (img && img.tagName === "IMG" && img.classList.contains("kt-img") && !img.dataset.fallback) {
        img.dataset.fallback = "1";              // only swap once, to avoid a loop
        img.src = "/images/placeholder.svg";
    }
}, true); // capture phase: image error events do not bubble
