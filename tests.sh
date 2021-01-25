rm -r testresults
docker-compose -f "docker-compose.yml" -f "docker-compose.tests.yml" up --build --force-recreate --abort-on-container-exit
docker cp tests-container:/out/testresults testresults
