import requests
import itertools
import random


class RequestGenerator(object):
    
    endpoints = [
        ('/api/users', 0.1),
        ('/api/authentication', 0.2),
        ('/api/posts', 0.3),
        ('/api/logout', 0.4),
    ]

    def __init__(self, uri):
        self.uri = uri
        self.probabilities = [p for e,p in self.endpoints]

    def generate_random_endpoint(self):
        totals = list(itertools.accumulate(self.probabilities))

        n = random.uniform(0, totals[-1])
        for i, total in enumerate(totals):
            if n <= total:
                return i

    def make_random_requests(self, num_requests):
        i=0
        while i < num_requests:
            random_uri = self.endpoints[self.generate_random_endpoint()][0]
            resp = requests.get(self.uri + random_uri)
            print(resp)
            i += 1

if __name__ == '__main__':
    generator = RequestGenerator('http://45.79.134.56')
    generator.make_random_requests(10)
