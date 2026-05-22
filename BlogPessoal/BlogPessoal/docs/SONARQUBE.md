# Guia SonarQube

## Subir SonarQube localmente

```bash
docker compose up -d sonarqube
```

Acesse:

```text
http://localhost:9000
```

Login padrão inicial do SonarQube:

```text
usuário: admin
senha: admin
```

Após o primeiro acesso, crie um token de análise e configure:

```bash
export SONAR_TOKEN="SEU_TOKEN"
```

## Executar análise

```bash
bash scripts/run-sonar.sh
```

## O que observar no relatório

- Bugs: falhas prováveis no código.
- Vulnerabilidades: problemas de segurança.
- Code smells: pontos de melhoria e legibilidade.
- Coverage: cobertura dos testes automatizados.
- Duplications: código duplicado.
- Maintainability Rating: facilidade de manutenção.

## Melhorias contínuas

Após cada análise, priorize:

1. Corrigir vulnerabilidades.
2. Corrigir bugs.
3. Reduzir duplicações.
4. Melhorar trechos com alta complexidade.
5. Ampliar a cobertura de testes.
