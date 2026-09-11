(() => {
  if (typeof hljs === "undefined") return;
  document.querySelectorAll("pre > code").forEach((block) => {
    block.classList.add("language-csharp");
    hljs.highlightElement(block);
  });
})();
