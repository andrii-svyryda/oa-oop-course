(() => {
  const slides = [...document.querySelectorAll(".slide")];
  const progress = document.querySelector(".progress");
  const counter = document.querySelector("[data-counter]");
  const total = slides.length;
  let i = Number(location.hash.replace("#", "")) || 0;
  if (i < 0 || i >= total) i = 0;
  let overview = false;

  function render() {
    slides.forEach((s, idx) => s.classList.toggle("active", idx === i && !overview));
    if (progress) progress.style.width = `${((i + 1) / total) * 100}%`;
    if (counter) counter.textContent = `${i + 1} / ${total}`;
    if (!overview) history.replaceState(null, "", `#${i}`);
  }

  function go(n) {
    if (overview) return;
    i = Math.max(0, Math.min(total - 1, n));
    render();
  }

  function toggleOverview() {
    overview = !overview;
    document.body.classList.toggle("overview-on", overview);
    slides.forEach((s) => s.classList.add("active"));
    if (!overview) render();
  }

  document.addEventListener("keydown", (e) => {
    if (["INPUT", "TEXTAREA"].includes(e.target.tagName)) return;
    if (e.key === "ArrowRight" || e.key === "PageDown" || e.key === " ") { e.preventDefault(); go(i + 1); }
    if (e.key === "ArrowLeft" || e.key === "PageUp" || e.key === "Backspace") { e.preventDefault(); go(i - 1); }
    if (e.key === "Home") go(0);
    if (e.key === "End") go(total - 1);
    if (e.key === "f" || e.key === "F") document.documentElement.requestFullscreen?.();
    if (e.key === "o" || e.key === "O" || e.key === "Escape") {
      if (e.key === "Escape" && !overview) return;
      toggleOverview();
    }
  });

  document.addEventListener("click", (e) => {
    if (e.target.closest("a, button, pre, code")) return;
    if (overview) {
      const slide = e.target.closest(".slide");
      if (!slide) return;
      i = slides.indexOf(slide);
      overview = false;
      document.body.classList.remove("overview-on");
      render();
      return;
    }
    const x = e.clientX / window.innerWidth;
    if (x > 0.72) go(i + 1);
    else if (x < 0.28) go(i - 1);
  });

  let touchX = null;
  document.addEventListener("touchstart", (e) => { touchX = e.changedTouches[0].clientX; }, { passive: true });
  document.addEventListener("touchend", (e) => {
    if (touchX == null) return;
    const dx = e.changedTouches[0].clientX - touchX;
    if (dx < -40) go(i + 1);
    if (dx > 40) go(i - 1);
    touchX = null;
  });

  render();
})();
