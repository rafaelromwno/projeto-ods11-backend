module.exports = {
    content: [
        './Pages/**/*.cshtml',
        './Views/**/*.cshtml'
    ],
    theme: {
        extend: {
            colors: {
                'verde-paleta': '#5BC561',
                'verde-escuro-paleta': '#3B8C4B',
                'azul-paleta': '#112B3C',
                'vermelho-paleta': '#A92C2C',
                'cinza-paleta': '#CED8E2',
            },
            backgroundImage: {
                'fundo1': "url('../src/img/imagem-fundo.jpg')",
                'fundo2': "url('../src/img/imagem-fundo-verde.jpg')",
            },
        },
    },
    plugins: [],
}

